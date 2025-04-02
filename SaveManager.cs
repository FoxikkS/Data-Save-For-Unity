using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class PlayerDataSave
{
    private static readonly string savePath = Application.persistentDataPath + "/SavePlayerData.json";

    public static void Save<TData>(TData data)
    {
        if (data == null)
        {
            Debug.Log($"Попытка сохранения null -> Тип: {typeof(TData).Name}");
            return;
        }

#if !UNITY_EDITOR
        const Formatting formatting = Formatting.None;
#else
        const Formatting formatting = Formatting.Indented;
#endif
        try
        {
            var json = JsonConvert.SerializeObject(data, formatting);

            EnsureDirectoryExists();
            
            File.WriteAllText(savePath, json);

            Debug.Log($"Данные сохранены: {savePath}");
        }
        catch (JsonException e)
        {
            Debug.LogError($"Ошибка при сериализации данных. data: {data}, type: {typeof(TData)} | Exception: {e}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка при сохранении данных. data: {data}, type: {typeof(TData)} | Exception: {e}");
        }
    }

    public static TData Load<TData>(TData defaultData = default)
    {
        try
        {
            EnsureDirectoryExists();

            if (!File.Exists(savePath))
            {
                Debug.LogWarning("Файл сохранения не найден!");
                return defaultData;
            }

            var json = File.ReadAllText(savePath);
            var data = JsonConvert.DeserializeObject<TData>(json);

            return data;
        }
        catch (JsonException e)
        {
            Debug.LogError($"Ошибка при десериализации данных: {e}");
            return defaultData;
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка при загрузке данных: {e}");
            return defaultData;
        }
    }

    private static void EnsureDirectoryExists()
    {
        var directory = Path.GetDirectoryName(savePath);

        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }
}
