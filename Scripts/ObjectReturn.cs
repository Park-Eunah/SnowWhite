using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturn : MonoBehaviour
{
    public float returnDelay = 0;

    private float timeCount = 0;

    private bool isReturn = false;

    private Rigidbody objRigidbody = null;
    private Transform objTransform = null;

    private Vector3 myOriginPosition = Vector3.zero;
    private Quaternion myOriginiRotation = Quaternion.identity;

    void Start()
    {
        objRigidbody = GetComponent<Rigidbody>();
        objTransform = GetComponent<Transform>();

        myOriginPosition = objTransform.position;
        myOriginiRotation = objTransform.rotation;
    }

    void Update()
    {
        if (isReturn)
        {
            if (Time.time > timeCount)
            {
                ReturnObject();
            }
        }
    }

    private void ReturnObject()
    {
        objRigidbody.velocity = Vector3.zero;
        objRigidbody.angularVelocity = Vector3.zero;

        objTransform.position = myOriginPosition;
        objTransform.rotation = myOriginiRotation;

        isReturn = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InteractionObject") || collision.gameObject.CompareTag("Origin"))
        {
            return;
        }

        timeCount = Time.time + returnDelay;
        isReturn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Controller"))
        {
            isReturn = false;
        }
    }
}
