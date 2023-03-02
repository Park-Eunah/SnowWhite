using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//상세설명창의 3d 오브젝트 프리팹에 들어갈 스크립트
public class InformationItem : MonoBehaviour
{
    private float rotateSpeed = 90f;
    private Vector2 thumbStick = Vector2.zero;
    private Quaternion originRotate = Quaternion.identity;

    private void OnEnable()
    {
        if (originRotate != Quaternion.identity)
        {
            transform.rotation = originRotate;
        }
    }

    private void Start()
    {
        originRotate = transform.rotation;
    }

    void Update()
    {
        Rotation();
    }

    private void Rotation() 
    {
        thumbStick = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        transform.Rotate(new Vector3(-thumbStick.y, -thumbStick.x, 0) * rotateSpeed * Time.deltaTime);
    }
}
