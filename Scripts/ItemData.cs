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
    public void SetItemData(Item item) //�κ��丮 ���Կ� ������ ������ �ֱ� ����.
    {
        this.item = item;
    }

    public Item GetItemData() //�κ��丮�� ������ ���� �������� ������ ��� ����.
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
