using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData_Manager : MonoBehaviour
{
    public static ItemData_Manager instance;

    public GameObject[] items;
    public Sprite[] ItemIcons;

    public TextAsset itemData_csvFile;

    Dictionary<int, Item> itemData = new Dictionary<int, Item>(); //각 아이템의 id값을 키값으로 함.

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        LoadItemData("ItemData");
        SetActiveItems();
    }

    private void LoadItemData(string _CSVFileName) //csv파일에서 아이템 정보들을 딕셔너리에 넣어준다
    {
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        Debug.Log(csvData.text);

        string[] data = csvData.text.Split('\n'); //CSV파일에서 엔터기준으로 쪼개준다

        for (int i = 1; i < data.Length; i++)
        {
            if (data.Equals(""))
            {
                break;
            }
            string[] row = data[i].Split(','); //줄마다 쉼표를 기준으로 끊어줌(맨 윗줄은 건너뜀)
            Debug.Log(row[0]);
            Debug.Log(row[0] + " : " + row[1]);
            itemData.Add(i, new Item(i, row[0], row[1])); //쉼표로 끊어준 내용들을 딕셔너리에 넣어줌
        }
    }

    public Item FindItemData(int itemID) //키값으로 아이템 찾기.
    {
        if (itemData.TryGetValue(itemID, out Item item))
        {
            return item;
        }
        else
            return null;
    }

    private void SetActiveItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(true);
        }
    }
}
