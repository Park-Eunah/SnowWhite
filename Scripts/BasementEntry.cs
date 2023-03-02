using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementEntry : MonoBehaviour
{
    public GameObject lock_;
    public GameObject door;

    private bool isOpened = false;

    private Animator anim = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Open()
    {
        if (!isOpened)
        {
            isOpened = true;
            SoundManager.instance.PlaySoundEffect(4);
            anim.SetBool("isOpen", true);
            door.GetComponent<MeshCollider>().enabled = true;
            lock_.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
