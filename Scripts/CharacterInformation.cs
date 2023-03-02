using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo
{
    private string name;
    private string info;

    public CharacterInfo(string name, string info)
    {
        this.name = name;
        this.info = info.Replace("'", ","); 
    }

    public string GetName()
    {
        return name;
    }

    public string GetInfo()
    {
        return info;
    }
}

public class CharacterInformation : MonoBehaviour
{
    Dictionary<int, CharacterInfo> characterInformation = new Dictionary<int, CharacterInfo>(); //각 아이템의 id값을 키값으로 함.

    void Awake()
    {
        LoadItemData("CharactersExplain");
    }

    private void LoadItemData(string _CSVFileName) //csv파일에서 아이템 정보들을 딕셔너리에 넣어준다
    {
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        Debug.Log(csvData.text);

        string[] data = csvData.text.Split('\n'); //CSV파일에서 엔터기준으로 쪼개준다

        for (int i = 0; i < data.Length; i++)
        {
            if (data.Equals(""))
            {
                break;
            }

            string[] row = data[i].Split(','); //줄마다 쉼표를 기준으로 끊어줌(맨 윗줄은 건너뜀)
            for (int j = 0; j < row.Length; j++)
            {
                row[j] = row[j].Replace("'", ","); //작은 따옴표를 쉼표로 변환.
            }
            Debug.Log(data[i]);
            Debug.Log(row[0]);
            characterInformation.Add(i, new CharacterInfo(row[0], row[1])); //쉼표로 끊어준 내용들을 딕셔너리에 넣어줌
        }
    }

    public CharacterInfo FindCharactorInformation(int charactorNum)
    {
        if (characterInformation.TryGetValue(charactorNum, out CharacterInfo characterInform))
        {
            return characterInform;
        }
        else
            return null; 
    }
}
