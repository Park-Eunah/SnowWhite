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

    private void OnTriggerEnter(Collider other) //뷰포트 안에 들어오면 콜라이더 활성화
    {
        if (other.CompareTag("Viewport"))
        {
            GetComponent<Collider>().enabled = true; ;
        }
    }

    private void OnTriggerExit(Collider other) //뷰포트를 벗어나면 콜라이더 비활성화
    {
        if (other.CompareTag("Viewport"))
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}
