using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject inventory;
    public GameObject charactor;
    public GameObject detect;
    public GameObject setting;
    public GameObject settings;
    public GameObject tutorial;

    public Button invenBtn;
    public Button characterBtn;
    public Button detectBtn;
    public Button settingBtn;

    public TMP_Text guideTxt;

    private float guideFadeTime = 0.5f;

    private string notYet = "아직 범인을 지목할 수 없습니다.";
    private string chooseCriminal = "누가 범인인가요?";

    private Item[] invenItems = null;

    public void OnClickInventoryBtn()
    {
        invenItems = GameManager.instance.GetInvenItems();
        if(invenItems != null)
        {
            inventory.GetComponent<Inventory>().OpenSlot(GameManager.instance.GetInvenItems());
        }
        invenBtn.Select();
        inventory.SetActive(true);
        charactor.SetActive(false);
        detect.SetActive(false);
        setting.SetActive(false);
        SoundManager.instance.PlaySoundEffect(7);
    }

    public void OnClickCharactorBtn()
    {
        characterBtn.Select();
        inventory.SetActive(false);
        charactor.SetActive(true);
        detect.SetActive(false);
        setting.SetActive(false);
        SoundManager.instance.PlaySoundEffect(7);
    }

    public void OnClickDetectBtn()
    {
        detectBtn.Select();
        inventory.SetActive(false);
        charactor.SetActive(false);
        detect.SetActive(true);
        setting.SetActive(false);
        SoundManager.instance.PlaySoundEffect(7);

        if (!GameManager.instance.IsBasementOpen())
        {
            guideTxt.text = notYet;
        }
        else
        {
            guideTxt.text = chooseCriminal;
            detect.GetComponent<DetectCriminal>().ShowStep1();
        }
    }

    public void OnClickSettingBtn()
    {
        settingBtn.Select();
        inventory.SetActive(false);
        charactor.SetActive(false);
        detect.SetActive(false);
        setting.SetActive(true);
        SoundManager.instance.PlaySoundEffect(7);

        if (tutorial.activeSelf)
        {
            tutorial.SetActive(false);
            settings.SetActive(true);
        }
    }

    public void Back()
    {
        if (inventory.activeSelf)
        {
            inventory.GetComponent<Inventory>().ShowItems();
        }
        else if (detect.activeSelf)
        {
            detect.GetComponent<DetectCriminal>().Back();
        }
        else if (tutorial.activeSelf)
        {
            tutorial.SetActive(false);
            settings.SetActive(true);
        }
    }

    public void ShowTutorial()
    {
        settings.SetActive(false);
        tutorial.SetActive(true);
    }
}
