using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//���Ͻ� ���� ������ �߸� ����.
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool isStart = false; //Ʃ�丮�� �� true�� �ٲ�
    public bool isCorrectCrimanal = false; //�´� ������ �����ߴ���
    public bool isCorrectClues = false; //�´� ���Ÿ� �����ߴ���

    public  int suspectNum = 0;

    private Scene currentScene;
    private Item[] invenItems = new Item[20];
    private int invenItemsIndex = 0;

    private bool isBasementOpen = false; //���Ͻ��� ���ȴ��� Ȯ���ϱ� ���� ����
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
        ChangeScene(1); //�߿ܾ����� �̵�
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

        //�´� �������� Ȯ��
    }

    public void ChangeScene(int sceneIndex)
    {
        currentScene = SceneManager.GetSceneByBuildIndex(sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }

    public int GetCurrnetScene() //����Ʈ���� ���¿��� ���� ���� Ȯ���ϱ� ����
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
        SceneManager.LoadScene(0); //Ÿ��Ʋ ������ ���ư�
        Destroy(this);
    }
}
