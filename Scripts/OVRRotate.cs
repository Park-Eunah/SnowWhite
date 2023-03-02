using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRRotate : MonoBehaviour
{
    private float x = 0f;
    private float rotateAngle = 20f;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.LTouch))
        {
            transform.Rotate(0, -rotateAngle, 0);
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.LTouch))
        {
            transform.Rotate(0, rotateAngle, 0);
        }
    }
}
