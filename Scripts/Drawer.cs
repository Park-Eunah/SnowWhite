using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    private bool isOpen = false;
    private Animator anim = null;
    private Lock lock_ = null;

    private void Start()
    {
        anim = GetComponent<Animator>();
        lock_ = GetComponentInChildren<Lock>();
    }

    public void OpenOrClose()
    {
        if (lock_ != null) //잠겨있으면 리턴
        {
            if (lock_.isLock)
            {
                return;
            }
        }

        isOpen = !isOpen;
        anim.SetBool("isOpen", isOpen);
        if (gameObject.name.Contains("Drawer"))
        {
            SoundManager.instance.PlaySoundEffect(3); 
        }
    }
}
