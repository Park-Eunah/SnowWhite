using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRHandAnimation : MonoBehaviour
{
    private Animator anim = null;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            anim.SetBool("IsGrip", true);
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            anim.SetBool("IsGrip", false);
        }
        if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            anim.SetBool("IsTrigger", true); 
        }
        if(OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            anim.SetBool("IsTrigger", false);
        }
    }
}
