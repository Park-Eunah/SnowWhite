using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRFade : MonoBehaviour
{
    public float fadeTime = 2f;

    private float changeValue = 0;

    private Material fadeMaterial = null;

    private Coroutine currentCoroutine = null;

    private static OVRFade instance;

    public static OVRFade Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        fadeMaterial = GetComponent<MeshRenderer>().material;
    }

    public void FadeIn()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(FadeOutCoroutine());
    }

    public void AutoFade()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(AutoFadeCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        Color fadeColor = fadeMaterial.color;

        while (fadeMaterial.color.a > 0)
        {
            changeValue -= Time.deltaTime / fadeTime;

            fadeColor.a = Mathf.Lerp(0, 1, changeValue);
            fadeMaterial.color = fadeColor;
            yield return null;
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        Color fadeColor = fadeMaterial.color;

        while (fadeMaterial.color.a < 1f)
        {
            changeValue += Time.deltaTime / fadeTime;

            fadeColor.a = Mathf.Lerp(0, 1, changeValue);
            fadeMaterial.color = fadeColor;
            yield return null;
        }
    }

    private IEnumerator AutoFadeCoroutine()
    {
        Color fadeColor = fadeMaterial.color;

        while (fadeMaterial.color.a < 1f)
        {
            changeValue += Time.deltaTime / fadeTime * 2;

            fadeColor.a = Mathf.Lerp(0, 1, changeValue);
            fadeMaterial.color = fadeColor;
            yield return null;
        }

        while (fadeMaterial.color.a > 0)
        {
            changeValue -= Time.deltaTime / fadeTime * 2;

            fadeColor.a = Mathf.Lerp(0, 1, changeValue);
            fadeMaterial.color = fadeColor;
            yield return null;
        }
    }
}
