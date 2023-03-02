using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public GameObject[] key;

    public Transform keyPos;

    public bool isLock = true;

    private string myTag = "";
    private Animator anim = null;

    private void Start()
    {
        myTag = tag;
        anim = GetComponentInChildren<Animator>();
    }

    private void SetLock()
    {
        switch (myTag)
        {
            case "Key1":
                if (GameManager.instance.IsBasementOpen())
                {
                    for (int i = 0; i < key.Length; i++)
                    {
                        key[i].SetActive(false);
                    }

                    gameObject.SetActive(false);
                }
                break;

            case "Key2":
                if (GameManager.instance.IsKey2Open())
                {
                    for (int i = 0; i < key.Length; i++)
                    {
                        key[i].SetActive(false);
                    }

                    gameObject.SetActive(false);
                }
                break;

            case "Key3":
                if (GameManager.instance.IsKey3Open())
                {
                    for (int i = 0; i < key.Length; i++)
                    {
                        key[i].SetActive(false);
                    }

                    gameObject.SetActive(false);
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (isLock && other.CompareTag(myTag)) //����ִ� ���¿��� Ʈ���� �ݶ��̴��� ���� �±��� ���谡 ������ �ִϸ��̼����� �ڹ��� �����ֱ�.
        {
            //���� ���ۿ� ���� �Ⱦ��ֱ�
            other.transform.SetParent(keyPos);
            other.transform.position = Vector3.zero;
            other.transform.rotation = Quaternion.identity;

            //ȿ���� ���
            SoundManager.instance.PlaySoundEffect(8);

            //�ڹ��� ���� �ִϸ��̼�
            anim.SetBool("isOpen", true);

            //��� �ִ��� Ȯ���ϴ� ���� �� ����.
            isLock = false;

            //���Ͻ��� �������� �߸� ��� Ȱ��ȭ.
            if (gameObject.name.Contains("Basement")) 
            {
                GameManager.instance.BasementOpen();
            }
        }
    }
}
