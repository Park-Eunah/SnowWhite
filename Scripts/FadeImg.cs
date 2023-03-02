using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImg : MonoBehaviour
{
    private float fadeTime = 1f;
    private Color color;
    private Image img = null;


    void Start()
    {
        img = GetComponent<Image>();
        color = img.color;
        Fade.instance.FadeOut(fadeTime, color, img);
    }
}
