using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening_Title : MonoBehaviour
{
    public GameObject text;

    private bool isFadeOut = false;
    private float fadeTime = 2f;

    private Color color;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        color = image.color;
    }

    void Update()
    {
        CheckButton();
        Next();
    }

    private void CheckButton()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            text.SetActive(false);
            SoundManager.instance.PlaySoundEffect(0); //게임시작 효과음
            Fade.instance.FadeOut(fadeTime, color, image);
            isFadeOut = true;
        }
    }

    private void Next()
    {
        if (isFadeOut == false)
        {
            return;
        }

        if(Fade.instance.IsCoroutining() == false)
        {
            GameManager.instance.GameStart();
        }
    }
}
