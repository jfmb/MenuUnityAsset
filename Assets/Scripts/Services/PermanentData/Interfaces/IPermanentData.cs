using System.Collections.Generic;

public interface IPermanentData
{
    public void LoadSingleData(string key);
    public void SaveSingleData(string key, string data);
    public void LoadGroupOfData(Dictionary<string, string> data);
    public void LoadGroupOfData(Dictionary<string, int> data);

    
    public void SaveGroupOfData(Dictionary<string, string> data);
    public void SaveGroupOfData(Dictionary<string, int> data);

    public void DeleteSingleData(string key);
    public void DeleteGroupOfData(List<string> keys);
    public void DeleteAllData();
}
