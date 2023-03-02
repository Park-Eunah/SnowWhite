using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�׷���ư(���) - ���� ���(GrabableObj), �ൿ(InteractionObj) 
public class OVRHand_ : MonoBehaviour
{
    public OVRInput.Controller controller;
    public Inventory inven;
    public Transform grabPos;

    private GameObject attachedObject; //��� �ִ� ������Ʈ(���̳� ������ �� ����)

    private OVRTeleport ovrTeleport = null;

    private List<GameObject> contactRigidboides = new List<GameObject>(); //�հ� ����ִ� ������Ʈ�� 

    private bool isAttached = false; //���� �հ� ����ִ� ������Ʈ�� �ִ��� Ȯ��
    private bool isPickUp = false; //���� ��� �ִ� ������ �ִ��� Ȯ��

    private void Start()
    {
        ovrTeleport = GetComponent<OVRTeleport>();
    }

    public void GetDownHandTrigger()
    {
        attachedObject = GetNearestRigidbody();

        if (attachedObject == null)
            return;

        if (attachedObject.TryGetComponent<GrabableObj>(out GrabableObj grabableObj))
        {
            ObjectPickUp();
        }
        else if (attachedObject.CompareTag("InteractionObj"))
        {
            Debug.Log("interactionObj");
            Action();
        }
    }

    public void ObjectPickUp() //�������
    {
        if (attachedObject == null) //���� ������ ������ ����.
            return;

        //���踦 �տ� ������ ȿ����
        if (attachedObject.tag.Contains("Key"))
        {
            SoundManager.instance.PlaySoundEffect(6);
        }

        isPickUp = true;
        if (controller == OVRInput.Controller.RTouch)
        {
            ovrTeleport.enabled = false;
        }

        attachedObject.transform.SetParent(grabPos);
        attachedObject.transform.position = Vector3.zero;

        attachedObject.transform.position = transform.position;
        attachedObject.transform.rotation = transform.rotation;

        if (attachedObject.TryGetComponent<ItemData>(out ItemData itemData)) //ó�� ������ �����̸� �κ��丮�� �־��ֱ�.
        {
            if (itemData.GetItemData().isFound == false)
            {
                Debug.Log(GameManager.instance.name);
                GameManager.instance.SaveItemData(itemData.GetItemData());
                itemData.Found();
                Debug.Log("save item data");
            }
        }
    }

    public void ObjectDrop()
    {
        if (isPickUp == false)
            return;

        isPickUp = false;
        if (controller == OVRInput.Controller.RTouch)
        {
            ovrTeleport.enabled = true;
        }

        attachedObject.GetComponent<GrabableObj>().ReturnObj();

        attachedObject.GetComponent<Outline>().enabled = false;

        attachedObject = null;

        isAttached = false;
    }

    public void Action() //�� ����, ������ ���� ��
    {
        if (attachedObject == null)
            return;

        if (attachedObject.TryGetComponent<Drawer>(out Drawer drawer)) //�հ� ����ִ� ������Ʈ�� DoorDrawer������Ʈ�� �ִٸ� 
        {
            drawer.OpenOrClose();
        }
        else if (attachedObject.TryGetComponent<BasementEntry>(out BasementEntry basementEntry))
        {
            basementEntry.Open();
        }
    }

    private GameObject GetNearestRigidbody() //���� ����� ������Ʈ ã��
    {
        GameObject nearestRigidbody = null;

        float minDistance = float.MaxValue;
        float distance = 0;

        foreach (GameObject rigidbody in contactRigidboides) //����Ʈ�� �ִ� ������Ʈ���� ���� �հ� ���� ����� ������Ʈ�� ã�Ƴ�.
        {
            distance = Vector3.Distance(rigidbody.transform.position, transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestRigidbody = rigidbody;
            }
        }

        return nearestRigidbody; //ã�Ƴ� ���� ����� ������ٵ� ��ȯ��.
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + other.name);
        if (other.CompareTag("InteractionObj") || other.TryGetComponent<GrabableObj>(out GrabableObj grabableObj))
        {
            contactRigidboides.Add(other.gameObject);

            if (other.TryGetComponent<Outline>(out Outline outline))
            {
               outline.enabled = true; //�ƿ����� �׷��ֱ�

                isAttached = true;
            }
            else
            {
                Outline outlline = other.GetComponentInChildren<Outline>();
                if (outline != null)
                {
                    outline.enabled = true;
                }
            }
            //Debug.Log("TriggerEnter  : " + attachedObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("InteractionObj") || other.TryGetComponent<GrabableObj>(out GrabableObj grabableObj))
        {
            contactRigidboides.Remove(other.gameObject);
            attachedObject = null;

            if (other.TryGetComponent<Outline>(out Outline outline))
            {
                outline.enabled = false; //�ƿ����� �����ֱ�
            }
            else if (other.GetComponentInChildren<Outline>() != null)
            {
                other.GetComponentInChildren<Outline>().enabled = false;
            }

            if (contactRigidboides.Count < 1)  //�� �̻� �հ� ����ִ� ������Ʈ�� ���ٸ� ���� �� �ִ� ������Ʈ�� ����
            {
                isAttached = false;
            }

            //Debug.Log("TriggerExit  : " + attachedObject);
        }
    }
}
