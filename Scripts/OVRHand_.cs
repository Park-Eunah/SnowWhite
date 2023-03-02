using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//그랩버튼(양손) - 물건 잡기(GrabableObj), 행동(InteractionObj) 
public class OVRHand_ : MonoBehaviour
{
    public OVRInput.Controller controller;
    public Inventory inven;
    public Transform grabPos;

    private GameObject attachedObject; //잡고 있는 오브젝트(문이나 서랍장 등 포함)

    private OVRTeleport ovrTeleport = null;

    private List<GameObject> contactRigidboides = new List<GameObject>(); //손과 닿아있는 오브젝트들 

    private bool isAttached = false; //현재 손과 닿아있는 오브젝트가 있는지 확인
    private bool isPickUp = false; //현재 잡고 있는 물건이 있는지 확인

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

    public void ObjectPickUp() //물겁잡기
    {
        if (attachedObject == null) //잡을 물건이 없으면 리턴.
            return;

        //열쇠를 손에 넣으면 효과음
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

        if (attachedObject.TryGetComponent<ItemData>(out ItemData itemData)) //처음 만지는 물건이면 인벤토리에 넣어주기.
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

    public void Action() //문 열기, 서랍장 열기 등
    {
        if (attachedObject == null)
            return;

        if (attachedObject.TryGetComponent<Drawer>(out Drawer drawer)) //손과 닿아있는 오브젝트에 DoorDrawer컴포넌트가 있다면 
        {
            drawer.OpenOrClose();
        }
        else if (attachedObject.TryGetComponent<BasementEntry>(out BasementEntry basementEntry))
        {
            basementEntry.Open();
        }
    }

    private GameObject GetNearestRigidbody() //가장 가까운 오브젝트 찾기
    {
        GameObject nearestRigidbody = null;

        float minDistance = float.MaxValue;
        float distance = 0;

        foreach (GameObject rigidbody in contactRigidboides) //리스트에 있는 오브젝트들을 비교해 손과 가장 가까운 오브젝트를 찾아냄.
        {
            distance = Vector3.Distance(rigidbody.transform.position, transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestRigidbody = rigidbody;
            }
        }

        return nearestRigidbody; //찾아낸 가장 가까운 리지드바디를 반환함.
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + other.name);
        if (other.CompareTag("InteractionObj") || other.TryGetComponent<GrabableObj>(out GrabableObj grabableObj))
        {
            contactRigidboides.Add(other.gameObject);

            if (other.TryGetComponent<Outline>(out Outline outline))
            {
               outline.enabled = true; //아웃라인 그려주기

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
                outline.enabled = false; //아웃라인 없애주기
            }
            else if (other.GetComponentInChildren<Outline>() != null)
            {
                other.GetComponentInChildren<Outline>().enabled = false;
            }

            if (contactRigidboides.Count < 1)  //더 이상 손과 닿아있는 오브젝트가 없다면 잡을 수 있는 오브젝트가 없음
            {
                isAttached = false;
            }

            //Debug.Log("TriggerExit  : " + attachedObject);
        }
    }
}
