using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspect : MonoBehaviour
{
    public DetectCriminal detect;

    public void OnClickButton()
    {
        detect.ChooseCriminal(transform.GetSiblingIndex());
    }
}
