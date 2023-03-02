using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//엔딩씬에 빈 오브젝트로 들어감
public class Ending : MonoBehaviour
{
    public TMP_Text resault;
    public TMP_Text laterStoryTxt;
    public LaterStory laterStory;
    public CharacterInformation characterInformation;

    private string criminalName = "";
    private string success = "당신은 추리에 성공하셨습니다.";
    private string failed = "당신은 추리에 실패하셨습니다..";

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
        //결과 텍스트
        if (GameManager.instance.isCorrectCrimanal) //범인을 맞췄을 때
        {
            if (GameManager.instance.isCorrectClues)
            {
                resault.text = success; //정답멘트
            }
            else if (!GameManager.instance.isCorrectClues)
            {
                resault.text = failed; //실패멘트
            }
        }
        else if (GameManager.instance.isCorrectCrimanal == false) //범인을 못 맞췄을 때
        {
            resault.text = failed;
        }

        Debug.Log(resault);

        //뒷이야기 텍스트
        laterStoryTxt.text = laterStory.FindLaterStory(++GameManager.instance.suspectNum);

        //지목한 범인에 따라 보여지는 뒷이야기가 달라짐.
        laterStoryTxt.text = laterStory.FindLaterStory(GameManager.instance.suspectNum);

        resault.gameObject.SetActive(true);
        laterStoryTxt.gameObject.SetActive(true);

        Debug.Log(laterStoryTxt.text);
    }

    //A 버튼을 누르면 맨 처음 타이틀 화면으로 돌아감
    private void EndGame()
    {
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)) 
        {
            GameManager.instance.EndGame();
        }
    }
}
