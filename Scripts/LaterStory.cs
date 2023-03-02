using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaterStory : MonoBehaviour
{
    Dictionary<int, string> laterStories = new Dictionary<int, string>(); //각 아이템의 id값을 키값으로 함.

    void Awake()
    {
        LoadItemData("Ending_LaterStories");
    }

    private void LoadItemData(string _CSVFileName) //csv파일에서 아이템 정보들을 딕셔너리에 넣어준다
    {
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        Debug.Log(csvData.text);

        string[] data = csvData.text.Split('@'); //CSV파일에서 @기준으로 쪼개준다

        for (int i = 1; i <= data.Length; i++) //1~7, 백설공주~6난쟁이.
        {
            if (data.Equals(""))
            {
                break;
            }
            string[] row = data[i-1].Split(','); //줄마다 쉼표를 기준으로 끊어줌(맨 윗줄은 건너뜀)
            Debug.Log(row[0] + " : " + row[1]);
            laterStories.Add(i, row[1]); //쉼표로 끊어준 내용들을 딕셔너리에 넣어줌
        }
    }

    public string FindLaterStory(int criminal)
    {
        if (laterStories.TryGetValue(criminal, out string laterStory))
        {
            laterStory = laterStory.Replace("'", ",");
            return laterStory;
        }
        else
            return null;
    }
}
