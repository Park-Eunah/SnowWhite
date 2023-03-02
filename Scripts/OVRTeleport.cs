using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRTeleport : MonoBehaviour
{
    public GameObject cameraRig;
    public GameObject point;  //����ĳ��Ʈ ���� �� ǥ���� ������Ʈ

    public Transform raycastStart;

    public LayerMask rayLayer;

    private bool movable = false; //�ڷ���Ʈ �������� Ȯ��

    private float rayDist = 6f;

    private Vector3 teleportPos = Vector3.zero;

    private RaycastHit hit;
    private GameObject hitObj = null;

    private LineRenderer line;

    void Start()
    {
        line = this.gameObject.AddComponent<LineRenderer>();
        SettingLine();
    }

    void Update()
    {
        //SetTeleportPos();

        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).y > 0.4f) //������ Ʈ���� ��ư�� ������ �ڷ���Ʈ
        {
            SetTeleportPos();
        }
        else if(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).y < 0.2f)
        {
            Teleport();
        }
    }

    private void SettingLine()
    {
        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f);
        line.material = material;
        line.positionCount = 2; //������ �������� 2��
        line.startWidth = 0.001f; //������ ����
        line.endWidth = 0.001f; //������ ����
    }

    private void SetTeleportPos()
    {
        line.enabled = true;
        line.SetPosition(0, raycastStart.position); //������ ������ ��ġ

        //Debug.DrawRay(raycastStart.position, raycastTarget.position,Color.black, rayDist);
        if (Physics.Raycast(raycastStart.position, raycastStart.forward, out hit, rayDist, rayLayer))
        {

            if (hitObj != null && hitObj != hit.collider.gameObject)
            {
                if (hitObj.CompareTag("Door"))
                {
                    hitObj.GetComponent<Outline>().enabled = false;
                }
                hitObj = hit.collider.gameObject;
            }
            else if (hitObj == null)
            {
                hitObj = hit.collider.gameObject;
            }
            if (hit.collider.CompareTag("Ground"))
            {
                point.transform.position = hit.point;
                point.SetActive(true);
                teleportPos = hit.point;
                movable = true;
            }
            else if (hit.collider.CompareTag("Door"))
            {
                hitObj.GetComponent<Outline>().enabled = true;
                point.SetActive(false);
                movable = true;
            }
            else
            {
                movable = false;
                point.SetActive(false);
            }

            line.SetPosition(1, hit.point); //������ �� ������ ����ĳ��Ʈ�� ���� ����.
        }
        else //����ĳ��Ʈ�� �浹�� ������Ʈ�� ������
        {
            if (hitObj != null && hitObj.CompareTag("Door"))
            {
                hitObj.GetComponent<Outline>().enabled = false ;
            }
            hitObj = null;
            line.SetPosition(1, transform.position + (transform.forward * rayDist)); //������ �� ������ ����ĳ��Ʈ�� �Ÿ���ŭ.
            point.SetActive(false);
            teleportPos = Vector3.zero;
            movable = false;
        }
    }

    private void Teleport()
    {
        line.enabled = false;
        point.SetActive(false);
        if (movable)
        {
            if (hitObj.CompareTag("Door"))
            {
                hitObj.GetComponent<Door>().OpenDoor();
            }

            movable = false;
            point.SetActive(false);

            cameraRig.transform.position = new Vector3(teleportPos.x, cameraRig.transform.position.y, teleportPos.z);
            //cameraRig.transform.position += teleportPos;
        }
    }
}
