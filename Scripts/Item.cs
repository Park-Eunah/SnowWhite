using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int id = 0; //id�� csv���Ͽ� �������� ���� ������� 1���� ����.
    public string itemName = "";
    public string information = "";
    public bool isFound = false;

    public Item(int _id, string _name, string _information)
    {
        this.id = _id;
        this.itemName = _name;
        this.information = _information;
        this.isFound = false;
    }
}