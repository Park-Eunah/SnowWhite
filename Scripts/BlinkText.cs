using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkText : MonoBehaviour
{
    private float time = 0;

    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        Blink();
    }

    private void Blink()
    {
        if (time < 0.5f)
        {
            text.color = new Color(1, 1, 1, 1 - time);
        }
        else
        {
            text.color = new Color(1, 1, 1, time);
            if (time > 1f)
            {
                time = 0;
            }
        }

        time += Time.deltaTime;
    }
}
