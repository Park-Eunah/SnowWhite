using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int id = 0; //id는 csv파일에 아이템이 적힌 순서대로 1부터 시작.
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