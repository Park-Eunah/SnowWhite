using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//�������� �� ������Ʈ�� ��
public class Ending : MonoBehaviour
{
    public TMP_Text resault;
    public TMP_Text laterStoryTxt;
    public LaterStory laterStory;
    public CharacterInformation characterInformation;

    private string criminalName = "";
    private string success = "����� �߸��� �����ϼ̽��ϴ�.";
    private string failed = "����� �߸��� �����ϼ̽��ϴ�..";

    private CharacterInfo characterInfo = null;

    void Start()
    {
        SetCriminalName();
        ShowLaterStory();
    }

    private void Update()
    {
        EndGame();
    }

    private void SetCriminalName()
    {
        Debug.Log("SetCriminalName");
        characterInfo = characterInformation.FindCharactorInformation(GameManager.instance.suspectNum);
        criminalName = characterInfo.GetName();
        Debug.Log(criminalName);
    }

    private void ShowLaterStory()
    {
        Debug.Log("ShowLaterStory");
        //��� �ؽ�Ʈ
        if (GameManager.instance.isCorrectCrimanal) //������ ������ ��
        {
            if (GameManager.instance.isCorrectClues)
            {
                resault.text = success; //�����Ʈ
            }
            else if (!GameManager.instance.isCorrectClues)
            {
                resault.text = failed; //���и�Ʈ
            }
        }
        else if (GameManager.instance.isCorrectCrimanal == false) //������ �� ������ ��
        {
            resault.text = failed;
        }

        Debug.Log(resault);

        //���̾߱� �ؽ�Ʈ
        laterStoryTxt.text = laterStory.FindLaterStory(++GameManager.instance.suspectNum);

        //������ ���ο� ���� �������� ���̾߱Ⱑ �޶���.
        laterStoryTxt.text = laterStory.FindLaterStory(GameManager.instance.suspectNum);

        resault.gameObject.SetActive(true);
        laterStoryTxt.gameObject.SetActive(true);

        Debug.Log(laterStoryTxt.text);
    }

    //A ��ư�� ������ �� ó�� Ÿ��Ʋ ȭ������ ���ư�
    private void EndGame()
    {
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) 
        {
            GameManager.instance.EndGame();
        }
    }
}
