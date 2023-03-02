using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRTeleport : MonoBehaviour
{
    public GameObject cameraRig;
    public GameObject point;  //레이캐스트 닿은 곳 표시할 오브젝트

    public Transform raycastStart;

    public LayerMask rayLayer;

    private bool movable = false; //텔레포트 가능한지 확인

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

        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch).y > 0.4f) //오른쪽 트리거 버튼을 누르면 텔레포트
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
        line.positionCount = 2; //레이저 꼭짓점은 2개
        line.startWidth = 0.001f; //레이저 굵기
        line.endWidth = 0.001f; //레이저 굵기
    }

    private void SetTeleportPos()
    {
        line.enabled = true;
        line.SetPosition(0, raycastStart.position); //라인의 시작점 위치

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

            line.SetPosition(1, hit.point); //라인의 끝 지점은 레이캐스트와 닿은 지점.
        }
        else //레이캐스트에 충돌한 오브젝트가 없으면
        {
            if (hitObj != null && hitObj.CompareTag("Door"))
            {
                hitObj.GetComponent<Outline>().enabled = false ;
            }
            hitObj = null;
            line.SetPosition(1, transform.position + (transform.forward * rayDist)); //라인의 끝 지점은 레이캐스트의 거리만큼.
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
