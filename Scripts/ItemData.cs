using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField]
    private Item item;

    void Start()
    {
        SetItemData(ItemData_Manager.instance.FindItemData(item.id));
    }
    public void SetItemData(Item item) //인벤토리 슬롯에 아이템 정보를 넣기 위해.
    {
        this.item = item;
    }

    public Item GetItemData() //인벤토리에 아이템 넣을 아이템의 정보를 얻기 위해.
    {
        return item;
    }

    public int GetItemID()
    {
        return item.id;
    }

    public void Found()
    {
        item.isFound = true;
    }
}
