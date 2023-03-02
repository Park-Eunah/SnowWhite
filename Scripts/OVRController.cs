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
    public GameObject dot; //���콺 ������ ������ �ϴ� ��Ʈ

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
            //���� �ڵ�Ʈ���� ��ư
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller))
            {
                ovrHand.GetDownHandTrigger();
            }
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller))
            {
                ovrHand.ObjectDrop();
            }

            if (OVRInput.GetDown(OVRInput.Button.One) && controller == OVRInput.Controller.RTouch) //A ������ �޴�
            {
                //canvas.SetActive(true);
                //if(Physics.Raycast(raycastStart.position, raycastStart.forward, out hit, rayDist, uiLayer)) //��Ʈ�ѷ��� ���ϰ� �ִ� ������ �޴� ��ġ �ٲ���
                //{
                //    menu.transform.position = hit.point;
                //    Debug.Log(hit.point + "      " +menu.transform.position);
                //}

                uiHelper.SetActive(true);
                menuIcon.SetActive(true);
                SoundManager.instance.PlaySoundEffect(11);
                HandsActive(false); //�� ġ���
                //line.enabled = true;
                ovrRotate.enabled = false;
                isMenu = true;
            }
        }

        else
        {
            if (OVRInput.GetDown(OVRInput.Button.One) && controller == OVRInput.Controller.RTouch) //������ ���̽�ƽ �ٽ� ������ �޴� ������
            {
                CloseMenu();
            }

            else if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) && controller == OVRInput.Controller.RTouch) //������ �׷���ư ������ �ڷΰ���
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
    //    line.positionCount = 2; //������ �������� 2��
    //    line.startWidth = 0.001f; //������ ����
    //    line.endWidth = 0.001f; //������ ����
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
    //            if (hit.collider.gameObject != hitObj && normalSprite != null) //highlightSprite�� �ٲ��� sprite �ٽ� ��������
    //            {

    //                if (hitObj.CompareTag("Inventory"))
    //                {
    //                    hitObj.GetComponentInChildren<Image>().sprite = normalSprite;
    //                }

    //                hitObj.GetComponent<Image>().sprite = normalSprite;
    //                hitObj = hit.collider.gameObject;

    //            }
    //        }


    //        if (hit.collider.gameObject.TryGetComponent<Button>(out Button btn)) //����ĳ��Ʈ�� �浹���� ��ư sprite �ٲ��ֱ�
    //        {
    //            if (hit.collider.CompareTag("Inventory"))
    //            {
    //                Sprite itemSprite = hit.collider.gameObject.GetComponentInChildren<Image>().sprite;
    //                normalSprite = itemSprite;
    //                itemSprite = btn.spriteState.highlightedSprite;
    //            }

    //            else if(btn.spriteState.highlightedSprite != null && hit.collider.GetComponent<Image>().sprite != btn.spriteState.highlightedSprite) //highlightedSprite�� ������ �ٲ��ֱ�
    //            {
    //                normalSprite = hit.collider.GetComponent<Image>().sprite; //���� sprite ����
    //                btn.gameObject.GetComponent<Image>().sprite = btn.spriteState.highlightedSprite;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        line.SetPosition(1, transform.position + (transform.forward * rayDist)); //������ �� ������ ����ĳ��Ʈ�� �Ÿ���ŭ.
    //        dot.SetActive(false);
    //    }
    //}

    //private void Click()
    //{
    //    if (dot.activeSelf) //dot�� Ȱ��ȭ �Ǿ����� ���� Ŭ�� ����
    //    {
    //        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) //������ �ε���Ʈ���ŷ� Ŭ��
    //        {
    //            if (hit.collider.gameObject.TryGetComponent<Button>(out Button btn))
    //            {
    //                btn.onClick.Invoke(); //onClick �̺�Ʈ ȣ��
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
        HandsActive(true);  //�� �ٽ� ���̱�
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
