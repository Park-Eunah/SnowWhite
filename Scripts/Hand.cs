using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        //if (gameObject.name.Contains("L"))
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
            {
                Debug.Log("¿ÞÂÊ");
                other.transform.parent = gameObject.transform;
            }
        }
        //else if (gameObject.name.Contains("R"))
        {
            if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
            {
                Debug.Log("¿À¸¥ÂÊ");
                other.transform.parent = gameObject.transform;
            }
        }
    }
}
