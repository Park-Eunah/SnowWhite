using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������Ʈ�� ��
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioSource bgmPlayer;
    public AudioSource effectPlayer;
    //public AudioClip[] bgmClips;
    public AudioClip[] soundEffects;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void BGMVolumeUp()
    {
        if (bgmPlayer.volume <= 0.05f)
        {
            bgmPlayer.Play();
        }

        bgmPlayer.volume += 0.05f;
    }

    public void BGMVolumeDown()
    {
        bgmPlayer.volume -= 0.05f;

        if (bgmPlayer.volume <= 0.05f)
        {
            bgmPlayer.Stop();
        }
    }

    public void SoundEffectVolumeUp()
    {
        effectPlayer.volume += 0.05f;
    }

    public void SoundEffectVolumeDown()
    {
        effectPlayer.volume -= 0.05f;
    }

    //public void SetBGMClip(int sceneIndex)
    //{
    //    switch(sceneIndex)
    //    {
    //        case 0: //Ÿ��Ʋ
    //            bgmPlayer.clip = null;
    //            break;
    //        case 1: //�� �ܺ�
    //        case 2: //�� ����
    //            bgmPlayer.clip = bgmClips[0];
    //            break;
    //        case 3: //���� �����
    //            bgmPlayer.clip = bgmClips[1];
    //            break;
    //        case 4: //����
    //            bgmPlayer.clip = null;
    //            break;

    //    }
    //}

    public void PlaySoundEffect(int audioClipNum)
    {
        effectPlayer.PlayOneShot(soundEffects[audioClipNum]);
    }
}
