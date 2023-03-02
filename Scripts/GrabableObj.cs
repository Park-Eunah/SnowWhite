using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObj : MonoBehaviour
{
    private Vector3 originPos = Vector3.zero;
    private Quaternion originRot = Quaternion.identity;
    private Transform originParent = null;
    void Start()
    {
        originPos = transform.position;
        originRot = transform.rotation;
        originParent = transform.parent;
    }

    public void ReturnObj() //���ڸ��� ���ư���
    {
        transform.position = originPos;
        transform.rotation = originRot;
        transform.SetParent(originParent);
    }
}
