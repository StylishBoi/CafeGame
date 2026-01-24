using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveData.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        SavedStats stats = new SavedStats();
        
        formatter.Serialize(stream, stats);
        stream.Close();
    }

    public static SavedStats LoadStats()
    {
        string path = Application.persistentDataPath + "/SaveData.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            SavedStats data = formatter.Deserialize(stream) as SavedStats;
            stream.Close();
            
            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }
}
