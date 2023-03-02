using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//지하실 문이 열리면 추리 가능.
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool isStart = false; //튜토리얼 후 true로 바뀜
    public bool isCorrectCrimanal = false; //맞는 범인을 지못했는지
    public bool isCorrectClues = false; //맞는 증거를 지목했는지

    public  int suspectNum = 0;

    private Scene currentScene;
    private Item[] invenItems = new Item[20];
    private int invenItemsIndex = 0;

    private bool isBasementOpen = false; //지하실이 열렸는지 확인하기 위한 변수
    private bool isKey2Open = false;
    private bool isKey3Open = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }


    }

    public void GameStart()
    {
        ChangeScene(1); //야외씬으로 이동
        isStart = false;
    }

    public void BasementOpen()
    {
        isBasementOpen = true;
    }

    public void Key2Open()
    {
        isKey2Open = true;
    }

    public void Key3Open()
    {
        isKey3Open = true;
    }
    public bool IsBasementOpen()
    {
        return isBasementOpen;
    }

    public bool IsKey2Open()
    {
        return isKey2Open;
    }

    public bool IsKey3Open()
    {
        return isKey3Open;
    }

    public void Select(int suspectNum, int clue1, int clue2)
    {
        if (suspectNum == 1)
            isCorrectCrimanal = true;
        else
            isCorrectCrimanal = false;

        //맞는 증거인지 확인
    }

    public void ChangeScene(int sceneIndex)
    {
        currentScene = SceneManager.GetSceneByBuildIndex(sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }

    public int GetCurrnetScene() //돈디스트로이 상태에서 현재 씬을 확인하기 위함
    {
        return currentScene.buildIndex;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SaveItemData(Item item)
    {
        Debug.Log( "saveitemdata"+item.itemName+ item.information);

        invenItems[invenItemsIndex] = item;
        invenItemsIndex++;

        Debug.Log(invenItems[invenItems.Length - 1]);
    }

    public Item[] GetInvenItems()
    {
        Debug.Log("getinvenitems");
        if (invenItems != null)
        {
            return invenItems;
        }
        else
            return null;
    }

    public void EndGame()
    {
        instance = null;
        SceneManager.LoadScene(0); //타이틀 씬으로 돌아감
        Destroy(this);
    }
}
