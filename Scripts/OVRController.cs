using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OVRController : MonoBehaviour
{
    public OVRInput.Controller controller;
    public LayerMask uiLayer;
    public GameObject uiHelper;
    public OVRRotate ovrRotate;

    public Transform raycastStart;

    public GameObject menuIcon;
    public GameObject menuSquare;
    public GameObject[] hands;
    public GameObject dot; //마우스 포인터 역할을 하는 도트

    private bool isMenu = false;
    private float rayDist = 2f;
    private OVRHand_ ovrHand = null;
    private RaycastHit hit;
    private GameObject hitObj;
    private Sprite normalSprite = null;

    private LineRenderer line = null;

    void Start()
    {
        ovrHand = GetComponentInChildren<OVRHand_>();
        line = GetComponent<LineRenderer>();
        //SettingLine();
    }

    void Update()
    {
        CheckButton();
        //if (isMenu)
        //{
        //    UiRaycast();
        //    Click();
        //}
    }

    private void CheckButton()
    {
        if (!isMenu)
        {
            //양쪽 핸드트리거 버튼
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
            {
                ovrHand.GetDownHandTrigger();
            }
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
            {
                ovrHand.ObjectDrop();
            }

            if (OVRInput.GetDown(OVRInput.Button.One) && controller == OVRInput.Controller.RTouch) //A 누르면 메뉴
            {
                //canvas.SetActive(true);
                //if(Physics.Raycast(raycastStart.position, raycastStart.forward, out hit, rayDist, uiLayer)) //컨트롤러가 향하고 있는 쪽으로 메뉴 위치 바뀌줌
                //{
                //    menu.transform.position = hit.point;
                //    Debug.Log(hit.point + "      " +menu.transform.position);
                //}

                uiHelper.SetActive(true);
                menuIcon.SetActive(true);
                SoundManager.instance.PlaySoundEffect(11);
                HandsActive(false); //손 치우기
                //line.enabled = true;
                ovrRotate.enabled = false;
                isMenu = true;
            }
        }

        else
        {
            if (OVRInput.GetDown(OVRInput.Button.One) && controller == OVRInput.Controller.RTouch) //오른쪽 조이스틱 다시 누르면 메뉴 나가기
            {
                CloseMenu();
            }

            else if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) && controller == OVRInput.Controller.RTouch) //오른쪽 그랩버튼 누르면 뒤로가기
            {
                if (menuSquare.activeSelf)
                {
                    menuSquare.GetComponent<Menu>().Back();
                }
            }
        }
    }

    //private void SettingLine()
    //{
    //    Material material = new Material(Shader.Find("Standard"));
    //    material.color = new Color(0, 195, 255, 0.5f);
    //    line.material = material;
    //    line.positionCount = 2; //레이저 꼭짓점은 2개
    //    line.startWidth = 0.001f; //레이저 굵기
    //    line.endWidth = 0.001f; //레이저 굵기
    //}

    //private void UiRaycast()
    //{
    //    line.SetPosition(0, raycastStart.position);
    //    if (Physics.Raycast(transform.position, Vector3.forward, out hit, rayDist, uiLayer))
    //    {
    //        Debug.Log(hit.collider.gameObject.name);
    //        line.SetPosition(1, hit.point);
    //        dot.SetActive(true);
    //        dot.transform.position = hit.point;
    //        if (hitObj != null)
    //        {
    //            if (hit.collider.gameObject != hitObj && normalSprite != null) //highlightSprite로 바꿔진 sprite 다시 돌려놓기
    //            {

    //                if (hitObj.CompareTag("Inventory"))
    //                {
    //                    hitObj.GetComponentInChildren<Image>().sprite = normalSprite;
    //                }

    //                hitObj.GetComponent<Image>().sprite = normalSprite;
    //                hitObj = hit.collider.gameObject;

    //            }
    //        }


    //        if (hit.collider.gameObject.TryGetComponent<Button>(out Button btn)) //레이캐스트에 충돌중인 버튼 sprite 바꿔주기
    //        {
    //            if (hit.collider.CompareTag("Inventory"))
    //            {
    //                Sprite itemSprite = hit.collider.gameObject.GetComponentInChildren<Image>().sprite;
    //                normalSprite = itemSprite;
    //                itemSprite = btn.spriteState.highlightedSprite;
    //            }

    //            else if(btn.spriteState.highlightedSprite != null && hit.collider.GetComponent<Image>().sprite != btn.spriteState.highlightedSprite) //highlightedSprite가 있으면 바꿔주기
    //            {
    //                normalSprite = hit.collider.GetComponent<Image>().sprite; //원래 sprite 저장
    //                btn.gameObject.GetComponent<Image>().sprite = btn.spriteState.highlightedSprite;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        line.SetPosition(1, transform.position + (transform.forward * rayDist)); //라인의 끝 지점은 레이캐스트의 거리만큼.
    //        dot.SetActive(false);
    //    }
    //}

    //private void Click()
    //{
    //    if (dot.activeSelf) //dot가 활성화 되어있을 때만 클릭 가능
    //    {
    //        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) //오른쪽 인덱스트리거로 클릭
    //        {
    //            if (hit.collider.gameObject.TryGetComponent<Button>(out Button btn))
    //            {
    //                btn.onClick.Invoke(); //onClick 이벤트 호출
    //            }
    //        }
    //    }
    //}

    private void HandsActive(bool isActive)
    {
        for (int i = 0; i < hands.Length; i++)
        {
            hands[i].SetActive(isActive);
        }
    }

    public void CloseMenu()
    {
        isMenu = false;
        uiHelper.SetActive(false);
        HandsActive(true);  //손 다시 보이기
        ovrRotate.enabled = false;

        if (menuIcon.activeSelf)
        {
            menuIcon.GetComponent<MenuIcons>().OnClickMenu();
        }

        if (menuSquare.activeSelf)
        {
            menuSquare.SetActive(false);
        }
    }
}
