using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OVRGrabObject : MonoBehaviour
{
    private float raycastDistance = 1f; //������ �Ÿ�
    private RaycastHit hit; //�������� �浹�� ��ü
    private LineRenderer laser; //������
    private GameObject grabObj; //���� �տ� �ִ� ������Ʈ
    private GameObject pointedObj; //���� �������� ����Ű�� �ִ� ������Ʈ

    void Start()
    {
        laser = this.gameObject.AddComponent<LineRenderer>(); //��ũ��Ʈ�� ���Ե� ��ü�� ���� ������ ������Ʈ �߰�

        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f); 
        laser.material = material;
        laser.positionCount = 2; //������ �������� 2��
        laser.startWidth = 0.01f; //������ ����
        laser.endWidth = 0.01f; //������ ����
    }

    void Update()
    {
        if (grabObj == null) //�տ� ����ִ� ������ ���� ���� ������ �׸��� ����ĳ��Ʈ�� �浹 Ȯ���ϱ�
        {
            Laser();
        }
        //GrabObject();

        ControllerCheck();
    }

    private void Laser()
    {
        laser.SetPosition(0, transform.position); //�������� ������ ��ġ

        //Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green, 0.5f);

        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance)) //�������� �浹�� ������Ʈ�� �ִٸ�
        {
            pointedObj = hit.collider.gameObject; //������ �浹�� ��ü ���
            laser.SetPosition(1, hit.point); //��ü�� �⵹�� ������ �������� ������ �Ѵ�
        }
        else //�������� �浹�� ��ü�� ���ٸ� 
        {
            laser.SetPosition(1, transform.position + (transform.forward * raycastDistance)); //�������� �������� �浹 ���� �Ÿ���ŭ.

            if (pointedObj != null) //������ null�� �ƴ϶�� 
            {
                pointedObj = null; //������ null�� ������ش�.
            }
        }
    }

    private void ControllerCheck()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch)) //������ �׷���ư�� ������
        {
            if (pointedObj == null && grabObj == null) //����ĳ��Ʈ�� �浹�ǰ� �ִ� ������Ʈ�� �տ� ��� �ִ� ������Ʈ�� ���ٸ� ����.
            {
                return;
            }
            else if (pointedObj.CompareTag("GrabableObj") || grabObj.CompareTag("GrabableObj")) //pointedObj�� grabObj�� �±װ� grabableObj�� ������Ʈ�� �ִٸ�
            {
                GrabObject();
            }
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))//������ Ʈ���Ź�ư�� ������
        {
            if (pointedObj != null && pointedObj.CompareTag("InteractionObj")) //����ĳ��Ʈ�� �浹���� ������Ʈ�� �ְ�, �±װ� interationObj���
            {
                Action();
            }
        }
    }

    private void GrabObject()
    {
        if (grabObj == null) //���� ����ִ� ������Ʈ�� ���ٸ� �浹���� ������Ʈ ���
        {
            grabObj = pointedObj; //�������� �浹���� ������Ʈ�� grabObj ������ ���

            //pointedObj = null; //pointedObj���� ����ֱ�
            grabObj.transform.parent = this.gameObject.transform; //���� �ڽ����� ������ֱ�.

            grabObj.transform.position =transform.position;
            grabObj.transform.rotation = transform.rotation;
        }
        else if(grabObj != null) //���� ��� �ִ� ������Ʈ�� �ִٸ� ��� �ִ� ������Ʈ ���ڸ��� ����
        {
            Debug.Log("OVRGrabObject > GrabObject > grabObj != null");
            grabObj.transform.parent = null; //�տ��� �����
            grabObj.GetComponent<GrabableObj>().ReturnObj(); //���� ��ġ�� ���ư���
            grabObj = null; //���� ����ֱ�
        }
    }  

    private void Action() 
    {
        if (pointedObj.CompareTag("InteractionObj"))
        {
            //��ȣ�ۿ��� ���� ������Ʈ ��ũ��Ʈ ����� ������Ʈ�� �־��ְ� ������Ʈ���� ������Ʈ �ҷ��ͼ� �޼��� ȣ�����ֱ�
        }
    }
}
    