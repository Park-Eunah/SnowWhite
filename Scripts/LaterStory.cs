using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaterStory : MonoBehaviour
{
    Dictionary<int, string> laterStories = new Dictionary<int, string>(); //�� �������� id���� Ű������ ��.

    void Awake()
    {
        LoadItemData("Ending_LaterStories");
    }

    private void LoadItemData(string _CSVFileName) //csv���Ͽ��� ������ �������� ��ųʸ��� �־��ش�
    {
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        Debug.Log(csvData.text);

        string[] data = csvData.text.Split('@'); //CSV���Ͽ��� @�������� �ɰ��ش�

        for (int i = 1; i <= data.Length; i++) //1~7, �鼳����~6������.
        {
            if (data.Equals(""))
            {
                break;
            }
            string[] row = data[i-1].Split(','); //�ٸ��� ��ǥ�� �������� ������(�� ������ �ǳʶ�)
            Debug.Log(row[0] + " : " + row[1]);
            laterStories.Add(i, row[1]); //��ǥ�� ������ ������� ��ųʸ��� �־���
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
