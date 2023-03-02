using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCriminal : MonoBehaviour
{
    public GameObject step1;
    public GameObject step2;
    public Transform clues;
    //public GameObject[] suspects; 
    //public GameObject[] clues;

    private bool isClue1Selected = false, isClue2Selected = false;
    private int suspectNum = 0; //1 ~ 7, �鼳���� ~ ������ 6�� �������.
    private int clue1 = 0, clue2 = 0; //���õ� �ܼ���
    private int clueSelectTurn = 0;

    //private void OnEnable()
    //{
    //    step1.SetActive(true);
    //    step2.SetActive(false);
    //}

    public void ShowStep1()
    {
        step1.SetActive(true);
        step2.SetActive(false);
    }

    public void ChooseCriminal(int susNum)
    {
        suspectNum = susNum;
        GameManager.instance.suspectNum = suspectNum;

        step1.SetActive(false);
        step2.SetActive(true);
    }

    public void ChooseClues(int clueNum)
    {
        clueNum++; //������ id�� ���߱� ����

        if (!isClue1Selected)
        {
            clue1 = clueNum;
            isClue1Selected = true;
        }
        else if (!isClue2Selected)
        {
            clue2 = clueNum;
            isClue2Selected = true;
        }
        else if (isClue1Selected && isClue2Selected)
        {
            if (clueSelectTurn == 0)
            {
                clueSelectTurn++;
                clues.GetChild(clue1).GetComponent<Clue>().isSeleted = false;
                clue1 = clueNum;
            }
            else
            {
                clueSelectTurn = 0;
                clues.GetChild(clue2).GetComponent<Clue>().isSeleted = false;
                clues.GetChild(clue2).GetComponent<Clue>().ChangeColor();
                clue2 = clueNum;
            }
        }
    }

    public void Select()
    {
        GameManager.instance.Select(suspectNum, clue1, clue2);
    }

    public void CancelSelected(int clueNum)
    {
        if (clueNum + 1 == clue1)
        {
            clue1 = 0;
        }
        else if (clueNum + 1 == clue2)
        {
            clue2 = 0;
        }
    }

    public void Back()
    {
        step2.SetActive(false);
        step1.SetActive(true);
        suspectNum = 0;
        clue1 = 0;
        clue2 = 0;
    }
}
