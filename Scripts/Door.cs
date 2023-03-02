using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int nextSceneIndex;

    public void OpenDoor()
    {
        if (gameObject.name.Contains("Basement"))
        {
            if (GameManager.instance.IsBasementOpen() == false)
            {
                return;
            }
        }

        GameManager.instance.ChangeScene(nextSceneIndex);
    }
}
