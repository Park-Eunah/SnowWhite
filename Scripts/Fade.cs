using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public static Fade instance = null;

    private Coroutine currentCoroutine = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool IsCoroutining()
    {
        if (currentCoroutine != null)
        {
            return true;
        }
        else
            return false;
    }

    public void FadeIn(float fadeTime, Color color, Image image)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(FadeInCoroutine(fadeTime, color, image));
    }

    public void FadeIn(float fadeTime, Color color, Text text)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(FadeInCoroutine(fadeTime, color, text));
    }

    public void FadeOut(float fadeTime, Color color, Image image)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(FadeOutCoroutine(fadeTime, color, image));
    }

    public void FadeOut(float fadeTime, Color color, Text text)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(FadeOutCoroutine(fadeTime, color, text));
    }

    public void FadeInOut(float fadeTime, Color color, Text text)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(FadeInOutCoroutine(fadeTime, color, text));
    }

    private IEnumerator FadeInCoroutine(float fadeTime, Color color, Image image)
    {
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / fadeTime;
            image.color = color;
            Debug.Log("FadeIn");
            yield return null;
        }
        currentCoroutine = null;
    }

    private IEnumerator FadeInCoroutine(float fadeTime, Color color, Text text)
    {
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / fadeTime;
            text.color = color;
            Debug.Log("FadeIn");
            yield return null;
        }
        currentCoroutine = null;
    }

    private IEnumerator FadeOutCoroutine(float fadeTime, Color color, Image image)
    {
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / fadeTime;
            image.color = color;
            Debug.Log("FadeOut");
            yield return null;
        }
        currentCoroutine = null;
        Debug.Log(currentCoroutine);
    }

    private IEnumerator FadeOutCoroutine(float fadeTime, Color color, Text text)
    {
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / fadeTime;
            text.color = color;
            Debug.Log("FadeOut");
            yield return null;
        }
        currentCoroutine = null;
        Debug.Log(currentCoroutine);
    }

    private IEnumerator FadeInOutCoroutine(float fadeTime, Color color, Text text)
    {
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / fadeTime;
            text.color = color;
            Debug.Log("FadeOut");
            yield return null;
        }

        while (color.a < 1f)
        {
            color.a += Time.deltaTime / fadeTime;
            text.color = color;
            Debug.Log("FadeIn");
            yield return null;
        }
        currentCoroutine = null;
        Debug.Log(currentCoroutine);
    }
}
