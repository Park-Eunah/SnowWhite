using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public TMP_Text informTxt; //상세설명창에 띄울 설명 텍스트
    public TMP_Text nameTxt; //상세설명창에서 띄울 아이템 이름
    public GameObject information; //상세설명창
    public GameObject items; //아이템들
    public GameObject objectViewItems; //상세설명창에 띄울 단서들

    public GameObject[] slots;

    //public Sprite[] itemIcons; //아이템아이디 순서대로 넣어줌

    private int nextSlot = 0; //다음 채워줄 슬롯

    private void OnEnable()
    {
        information.SetActive(false);
        items.SetActive(true);
    }

    public void OpenSlot(Item[] item) //슬롯 열기
    {
        for(int i = 0; i<item.Length; i++)
        {
            if(item[i] == null)
            {
                return;
            }
            slots[nextSlot].GetComponent<ItemData>().SetItemData(item[i]); //매개변수로 받아온 아이템 정보 슬롯에 넣어주기
            slots[nextSlot].GetComponentInChildren<Image>().sprite = ItemData_Manager.instance.ItemIcons[slots[nextSlot].GetComponent<ItemData>().GetItemID() - 1]; //아이템에 맞는 이미지를 넣어줌.
            slots[nextSlot].GetComponentInChildren<Image>().gameObject.SetActive(true);
            slots[nextSlot].GetComponent<Button>().interactable = true; //슬롯을 상호작용 가능한 상태로 만들어준다.
            nextSlot++;
        }
    }

    public void ShowInformation(Item item) //단서 상세설명창 띄우기
    {
        informTxt.text = item.information; //받아온 아이템 정보 중 설명글을 텍스트에 입력해준다.
        nameTxt.text = item.itemName;//받아온 아이템 정보 중 아이템 이름을 텍스트에 입력해준다.
        items.SetActive(false);
        information.SetActive(true);
        objectViewItems.transform.GetChild(item.id - 1).gameObject.SetActive(true);
    }

    public void ShowItems()
    {
        information.SetActive(false);
        items.SetActive(true);
    }
}
