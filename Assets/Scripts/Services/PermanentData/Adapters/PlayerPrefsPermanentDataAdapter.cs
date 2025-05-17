using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsPermanentDataAdapter: IPermanentData
{
    public void LoadSingleData(string key)
    {
    }

    public void SaveSingleData(string key, string data)
    {
    }

    public void LoadGroupOfData(Dictionary<string, string> data)
    {
    }

    public void LoadGroupOfData(Dictionary<string, int> data)
    {
        foreach (var element in data)
        {
            if (!PlayerPrefs.HasKey(element.Key))
            {
                continue;
            }
            
            data[element.Key] = PlayerPrefs.GetInt(element.Key);
        }
    }

    public void SaveGroupOfData(Dictionary<string, string> data)
    {
    }

    public void SaveGroupOfData(Dictionary<string, int> data)
    {
        
    }

    public void SaveAllData(Dictionary<string, string> data)
    {
    }

    public void DeleteSingleData(string key)
    {
    }

    public void DeleteGroupOfData(List<string> keys)
    {
    }

    public void DeleteAllData()
    {
    }
}
