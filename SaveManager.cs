using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class PlayerDataSaver
{
    private static readonly string _savePath = Application.persistentDataPath + "/SavePlayerData.json";
    
    public static void Save<T>(T data)
    {
        try
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_savePath, json);
            Debug.Log($"Данные сохранены: {_savePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка при сохранении данных: {e}");
        }
    }

    public static T Load<T>()
    {
        try
        {
            if (File.Exists(_savePath)) // Теперь проверка корректная
            {
                string json = File.ReadAllText(_savePath);
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                Debug.LogWarning("Файл сохранения не найден!");
                return default;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка при загрузке данных: {e}");
            return default;
        }
    }
}