using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clue : MonoBehaviour
{
    public DetectCriminal detect;
    public GameObject invenItems;

    public bool isSeleted = false;

    private Button btn = null;
    private Image itemImg = null;

    private Color normalColor;
    private Color highlightedColor;

    private void OnEnable()
    {
        SettingItem();
    }

    void Start()
    {
        btn = GetComponent<Button>();
        itemImg = GetComponentInChildren<Image>();
        normalColor = btn.colors.normalColor;
        highlightedColor = btn.colors.normalColor;
    }

    public void OnClickButton()
    {
        isSeleted = !isSeleted;
        if (isSeleted)
        {
            btn.Select();
            detect.ChooseClues(transform.GetSiblingIndex());
        }
        else
        {
            Unselect(transform.GetSiblingIndex());
        }

        ChangeColor();
    }

    public void ChangeColor()
    {
        ColorBlock colorBlock = btn.colors;

        if (!isSeleted)
        {
            colorBlock.normalColor = normalColor;
            colorBlock.highlightedColor = highlightedColor;
            btn.colors = colorBlock;
        }
        else if(isSeleted)
        {
            colorBlock.normalColor = btn.colors.selectedColor;
            colorBlock.highlightedColor = btn.colors.selectedColor;
            btn.colors = colorBlock;
        }
    }

    private void Unselect(int clueNum)
    {
        detect.CancelSelected(clueNum);
    }

    private void SettingItem()
    {
        //ItemData item = invenItems.transform.GetChild(transform.GetSiblingIndex()).GetComponent<ItemData>();
        //itemImg.sprite = ItemData_Manager.instance.ItemIcons[item.GetItemID() - 1];
        itemImg.sprite = ItemData_Manager.instance.ItemIcons[GameManager.instance.GetInvenItems()[transform.GetSiblingIndex()].id];
        itemImg.gameObject.SetActive(true);
    }
}
