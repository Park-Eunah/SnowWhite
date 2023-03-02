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
    Dictionary<int, CharacterInfo> characterInformation = new Dictionary<int, CharacterInfo>(); //�� �������� id���� Ű������ ��.

    void Awake()
    {
        LoadItemData("CharactersExplain");
    }

    private void LoadItemData(string _CSVFileName) //csv���Ͽ��� ������ �������� ��ųʸ��� �־��ش�
    {
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        Debug.Log(csvData.text);

        string[] data = csvData.text.Split('\n'); //CSV���Ͽ��� ���ͱ������� �ɰ��ش�

        for (int i = 0; i < data.Length; i++)
        {
            if (data.Equals(""))
            {
                break;
            }

            string[] row = data[i].Split(','); //�ٸ��� ��ǥ�� �������� ������(�� ������ �ǳʶ�)
            for (int j = 0; j < row.Length; j++)
            {
                row[j] = row[j].Replace("'", ","); //���� ����ǥ�� ��ǥ�� ��ȯ.
            }
            Debug.Log(data[i]);
            Debug.Log(row[0]);
            characterInformation.Add(i, new CharacterInfo(row[0], row[1])); //��ǥ�� ������ ������� ��ųʸ��� �־���
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
