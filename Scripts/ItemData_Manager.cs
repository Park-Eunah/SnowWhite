using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData_Manager : MonoBehaviour
{
    public static ItemData_Manager instance;

    public GameObject[] items;
    public Sprite[] ItemIcons;

    public TextAsset itemData_csvFile;

    Dictionary<int, Item> itemData = new Dictionary<int, Item>(); //�� �������� id���� Ű������ ��.

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

    private void LoadItemData(string _CSVFileName) //csv���Ͽ��� ������ �������� ��ųʸ��� �־��ش�
    {
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        Debug.Log(csvData.text);

        string[] data = csvData.text.Split('\n'); //CSV���Ͽ��� ���ͱ������� �ɰ��ش�

        for (int i = 1; i < data.Length; i++)
        {
            if (data.Equals(""))
            {
                break;
            }
            string[] row = data[i].Split(','); //�ٸ��� ��ǥ�� �������� ������(�� ������ �ǳʶ�)
            Debug.Log(row[0]);
            Debug.Log(row[0] + " : " + row[1]);
            itemData.Add(i, new Item(i, row[0], row[1])); //��ǥ�� ������ ������� ��ųʸ��� �־���
        }
    }

    public Item FindItemData(int itemID) //Ű������ ������ ã��.
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
