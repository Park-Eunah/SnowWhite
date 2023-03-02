using UnityEngine;
using UnityEngine.UI;

public class Opening_Letter : MonoBehaviour
{
    public OVRController controller;

    private void Start()
    {
        if (GameManager.instance.isStart) //이전에 튜토리얼을 진행했었다면 없애기
        {
            Debug.Log(GameManager.instance.isStart);
            gameObject.SetActive(false);
        }
        SoundManager.instance.PlaySoundEffect(1);
    }

    private void Update()
    {
        Next();
    }

    private void Next()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            GameManager.instance.isStart = true;
            controller.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
