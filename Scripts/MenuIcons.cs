using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIcons : MonoBehaviour
{
    public GameObject menu;
    public GameObject canvas;

    public Animator[] menuIcons;

    private void OnEnable()
    {
        SoundManager.instance.PlaySoundEffect(11);
        for (int i = 0; i < menuIcons.Length; i++)
        {
            menuIcons[i].SetBool("isOpen", true);
        }
    }

    public void OnClickInventory()
    {
        Debug.Log("inven");
        menu.SetActive(true);
        menu.GetComponent<Menu>().OnClickInventoryBtn();
        gameObject.SetActive(false);
    }

    public void OnClickCharactor()
    {
        menu.SetActive(true);
        menu.GetComponent<Menu>().OnClickCharactorBtn();
        gameObject.SetActive(false);
    }

    public void OnClickDetect()
    {
        menu.SetActive(true);
        menu.GetComponent<Menu>().OnClickDetectBtn();
        gameObject.SetActive(false);
    }

    public void OnClickSetting()
    {
        menu.SetActive(true);
        menu.GetComponent<Menu>().OnClickSettingBtn();
        gameObject.SetActive(false);
    }

    public void OnClickMenu()
    {
        for(int i = 0; i < menuIcons.Length; i++)
        {
            menuIcons[i].SetBool("isOpen", false);
        }

        Invoke("CloseMenu", 1f);
    }

    private void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
