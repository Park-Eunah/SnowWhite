using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OVRGrabObject : MonoBehaviour
{
    private float raycastDistance = 1f; //레이저 거리
    private RaycastHit hit; //레이저에 충돌된 객체
    private LineRenderer laser; //레이저
    private GameObject grabObj; //현재 손에 있는 오브젝트
    private GameObject pointedObj; //현재 레이저가 가리키고 있는 오브젝트

    void Start()
    {
        laser = this.gameObject.AddComponent<LineRenderer>(); //스크립트가 포함된 객체에 라인 렌더러 컴포넌트 추가

        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f); 
        laser.material = material;
        laser.positionCount = 2; //레이저 꼭짓점은 2개
        laser.startWidth = 0.01f; //레이저 굵기
        laser.endWidth = 0.01f; //레이저 굵기
    }

    void Update()
    {
        if (grabObj == null) //손에 잡고있는 물건이 없을 때만 레이저 그리고 레이캐스트로 충돌 확인하기
        {
            Laser();
        }
        //GrabObject();

        ControllerCheck();
    }

    private void Laser()
    {
        laser.SetPosition(0, transform.position); //레이저의 시작점 위치

        //Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green, 0.5f);

        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance)) //레이저와 충돌한 오브젝트가 있다면
        {
            pointedObj = hit.collider.gameObject; //변수에 충돌한 물체 담기
            laser.SetPosition(1, hit.point); //물체와 출돌한 지점을 레이저의 끝으로 한다
        }
        else //레이저와 충돌한 물체가 없다면 
        {
            laser.SetPosition(1, transform.position + (transform.forward * raycastDistance)); //레이저의 끝지점을 충돌 감지 거리만큼.

            if (pointedObj != null) //변수가 null이 아니라면 
            {
                pointedObj = null; //변수를 null로 만들어준다.
            }
        }
    }

    private void ControllerCheck()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch)) //오른쪽 그랩버튼이 눌리면
        {
            if (pointedObj == null && grabObj == null) //레이캐스트에 충돌되고 있는 오브젝트와 손에 잡고 있는 오브젝트가 없다면 리턴.
            {
                return;
            }
            else if (pointedObj.CompareTag("GrabableObj") || grabObj.CompareTag("GrabableObj")) //pointedObj와 grabObj중 태그가 grabableObj인 오브젝트가 있다면
            {
                GrabObject();
            }
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))//오른쪽 트리거버튼이 눌리면
        {
            if (pointedObj != null && pointedObj.CompareTag("InteractionObj")) //레이캐스트에 충돌중인 오브젝트가 있고, 태그가 interationObj라면
            {
                Action();
            }
        }
    }

    private void GrabObject()
    {
        if (grabObj == null) //현재 잡고있는 오브젝트가 없다면 충돌중인 오브젝트 잡기
        {
            grabObj = pointedObj; //레이저에 충돌중인 오브젝트를 grabObj 변수에 담기

            //pointedObj = null; //pointedObj변수 비워주기
            grabObj.transform.parent = this.gameObject.transform; //손의 자식으로 만들어주기.

            grabObj.transform.position =transform.position;
            grabObj.transform.rotation = transform.rotation;
        }
        else if(grabObj != null) //현재 잡고 있는 오브젝트가 있다면 잡고 있는 오브젝트 제자리에 놓기
        {
            Debug.Log("OVRGrabObject > GrabObject > grabObj != null");
            grabObj.transform.parent = null; //손에서 벗어나기
            grabObj.GetComponent<GrabableObj>().ReturnObj(); //원래 위치로 돌아가기
            grabObj = null; //변수 비워주기
        }
    }  

    private void Action() 
    {
        if (pointedObj.CompareTag("InteractionObj"))
        {
            //상호작용을 위한 오브젝트 스크립트 만들어 오브젝트에 넣어주고 오브젝트에서 컴포넌트 불러와서 메서드 호출해주기
        }
    }
}
    