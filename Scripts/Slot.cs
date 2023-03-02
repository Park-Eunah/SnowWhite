using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Inventory inven;

    public void OnClickSlotBtn()
    {
        inven.ShowInformation(GetComponent<ItemData>().GetItemData());
    }

    private void OnTriggerEnter(Collider other) //����Ʈ �ȿ� ������ �ݶ��̴� Ȱ��ȭ
    {
        if (other.CompareTag("Viewport"))
        {
            GetComponent<Collider>().enabled = true; ;
        }
    }

    private void OnTriggerExit(Collider other) //����Ʈ�� ����� �ݶ��̴� ��Ȱ��ȭ
    {
        if (other.CompareTag("Viewport"))
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}
