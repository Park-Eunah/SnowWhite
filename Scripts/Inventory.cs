using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public TMP_Text informTxt; //�󼼼���â�� ��� ���� �ؽ�Ʈ
    public TMP_Text nameTxt; //�󼼼���â���� ��� ������ �̸�
    public GameObject information; //�󼼼���â
    public GameObject items; //�����۵�
    public GameObject objectViewItems; //�󼼼���â�� ��� �ܼ���

    public GameObject[] slots;

    //public Sprite[] itemIcons; //�����۾��̵� ������� �־���

    private int nextSlot = 0; //���� ä���� ����

    private void OnEnable()
    {
        information.SetActive(false);
        items.SetActive(true);
    }

    public void OpenSlot(Item[] item) //���� ����
    {
        for(int i = 0; i<item.Length; i++)
        {
            if(item[i] == null)
            {
                return;
            }
            slots[nextSlot].GetComponent<ItemData>().SetItemData(item[i]); //�Ű������� �޾ƿ� ������ ���� ���Կ� �־��ֱ�
            slots[nextSlot].GetComponentInChildren<Image>().sprite = ItemData_Manager.instance.ItemIcons[slots[nextSlot].GetComponent<ItemData>().GetItemID() - 1]; //�����ۿ� �´� �̹����� �־���.
            slots[nextSlot].GetComponentInChildren<Image>().gameObject.SetActive(true);
            slots[nextSlot].GetComponent<Button>().interactable = true; //������ ��ȣ�ۿ� ������ ���·� ������ش�.
            nextSlot++;
        }
    }

    public void ShowInformation(Item item) //�ܼ� �󼼼���â ����
    {
        informTxt.text = item.information; //�޾ƿ� ������ ���� �� ������� �ؽ�Ʈ�� �Է����ش�.
        nameTxt.text = item.itemName;//�޾ƿ� ������ ���� �� ������ �̸��� �ؽ�Ʈ�� �Է����ش�.
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
