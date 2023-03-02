using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewButton : MonoBehaviour
{
    public Animator items;

    private bool isLeft = true;
    //private float speed = 3f;
    //private float leftPosx = 0f;
    //private float rightPosx = 0f;
    //private float moveValue = 0f;
    //private Vector3 itemsPos = Vector3.zero;
    //private Vector3 movePos = Vector3.zero;
    //private RectTransform rectTransform = null;
    //private Coroutine currentCoroutine = null;

    void Start()
    {
        //rectTransform = items.GetComponent<RectTransform>();
        //itemsPos = items.transform.position;
        //leftPosx = itemsPos.x;
        //rightPosx = leftPosx - 0.65f;
    }

    public void Left()
	{
        if (isLeft)
        {
            return;
        }
        isLeft = true;
        items.SetBool("isLeft", true);
	}

    public void Right()
	{
        if (!isLeft)
        {
            return;
        }
        isLeft = false;
		items.SetBool("isLeft", false);
	}

 //   private void Move()
	//{
 //       Debug.Log("move");
	//	switch (isLeft)
	//	{
 //           case true:
 //               currentCoroutine = StartCoroutine(MoveLeft());
 //               break;
 //           case false:
 //               currentCoroutine = StartCoroutine(MoveRight());
 //               break;
	//	}
	//}

	//IEnumerator MoveLeft()
	//{
 //       movePos = items.transform.position;

 //       while (items.transform.position.x <= leftPosx)
	//	{
 //           movePos.x += speed * Time.deltaTime;
 //           items.transform.position = movePos;
 //           Debug.Log(leftPosx + "   " + items.transform.position);
 //           yield return null;
	//	}

 //       currentCoroutine = null;
	//}

 //   IEnumerator MoveRight()
 //   {
 //       movePos = items.transform.position;

 //       while (items.transform.position.x >= rightPosx)
 //       {
 //           movePos.x -= speed * Time.deltaTime;
 //           items.transform.position = movePos;
 //           Debug.Log(items.transform.position);
 //           yield return null;
 //       }

 //       currentCoroutine = null;
 //   }
}
