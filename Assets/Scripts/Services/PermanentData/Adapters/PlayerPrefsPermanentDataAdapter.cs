using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPrefsPermanentDataAdapter: IPermanentData
{
    public void LoadSingleData(string key)
    {
    }

    public void SaveSingleData(string key, string data)
    {
    }

    public void SaveSingleData(string key, int data)
    {
        PlayerPrefs.SetInt(key, data);
    }

    public void LoadGroupOfData(Dictionary<string, string> data)
    {
    }

    public void LoadGroupOfData(Dictionary<string, int> data)
    {
        var listOfKeys = data.Keys.ToList();
        foreach (var key in listOfKeys)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                continue;
            }
            
            data[key] = PlayerPrefs.GetInt(key);
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
        PlayerPrefs.DeleteAll();
    }
}
