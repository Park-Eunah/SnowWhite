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
    }

    void Update()
    {
        CheckButton();
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
                uiHelper.SetActive(true);
                menuIcon.SetActive(true);
                SoundManager.instance.PlaySoundEffect(11);
                HandsActive(false); //�� ġ���
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
