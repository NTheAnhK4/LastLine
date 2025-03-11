
using System.IO;
using UnityEngine;

public static class JsonSaveSystem
{
    private static string GetFilePath<T>() => Application.persistentDataPath + "/" + typeof(T) + ".json";

    public static void SaveData<T>(T data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetFilePath<T>(), json);
    }

    public static T LoadData<T>() where T : new()
    {
        string path = GetFilePath<T>();
        if (File.Exists(path))
        {
           
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }
        return new T(); 
    }
}
