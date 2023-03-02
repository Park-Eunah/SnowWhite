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
        if (isLock && other.CompareTag(myTag)) //잠겨있는 상태에서 트리거 콜라이더에 같은 태그의 열쇠가 들어오면 애니메이션으로 자물쇠 열어주기.
        {
            //열쇠 구멍에 열쇠 꽂아주기
            other.transform.SetParent(keyPos);
            other.transform.position = Vector3.zero;
            other.transform.rotation = Quaternion.identity;

            //효과음 재생
            SoundManager.instance.PlaySoundEffect(8);

            //자물쇠 여는 애니메이션
            anim.SetBool("isOpen", true);

            //잠겨 있는지 확인하는 변수 값 변경.
            isLock = false;

            //지하실이 열렸으면 추리 기능 활성화.
            if (gameObject.name.Contains("Basement")) 
            {
                GameManager.instance.BasementOpen();
            }
        }
    }
}
