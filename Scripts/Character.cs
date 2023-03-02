using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Character : MonoBehaviour
{
    public Button[] characters;
    public Sprite[] characterImages;
    public Image characterImage;
    public CharacterInformation charactorInformation;
    public TMP_Text name;
    public TMP_Text informationText;

    private CharacterInfo characterInfo;
    private string information;

    public void ShowCharactorInfo(int characterNum)
    {
        characterInfo = charactorInformation.FindCharactorInformation(characterNum);
        characterImage.sprite = characterImages[characterNum];
        name.text = characterInfo.GetName();
        informationText.text = characterInfo.GetInfo();
    }
}