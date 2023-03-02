using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterButton : MonoBehaviour
{
    public Character charactor;

    public void OnClickCharactorButton()
    {
        charactor.ShowCharactorInfo(transform.GetSiblingIndex());
    }
}
