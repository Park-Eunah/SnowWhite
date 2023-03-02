using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Setting : MonoBehaviour
{
    public GameObject settings;
    public GameObject tutorial;
    public TMP_Text bgmText;
    public TMP_Text sfxText;

    private void OnEnable()
    {
        settings.SetActive(true);
        tutorial.SetActive(false);
    }

    public void BGMVolumeUp()
    {
        SoundManager.instance.BGMVolumeUp();
        bgmText.text = ((int)(SoundManager.instance.bgmPlayer.volume * 100 + 0.4f)).ToString();
    }

    public void BGMVolumeDown()
    {
        SoundManager.instance.BGMVolumeDown();
        bgmText.text = ((int)(SoundManager.instance.bgmPlayer.volume * 100 + 0.4f)).ToString();
    }

    public void SoundEffectVolumeUp()
    {
        SoundManager.instance.SoundEffectVolumeUp();
        sfxText.text = ((int)(SoundManager.instance.effectPlayer.volume * 100 + 0.4f)).ToString();
    }

    public void SoundEffectVolumeDown()
    {
        SoundManager.instance.SoundEffectVolumeDown();
        sfxText.text = ((int)(SoundManager.instance.effectPlayer.volume * 100 + 0.4f)).ToString();
    }

    public void ShowTutorial()
    {
        settings.SetActive(false);
        tutorial.SetActive(true);
    }

    public void QuitGame()
    {
        GameManager.instance.QuitGame();
    }
}
