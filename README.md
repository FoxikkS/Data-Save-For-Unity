# Data Saver for Unity

This system allows you to save and load data in Unity using JSON files. It supports saving and loading **any data type** with generic methods `<T>`.

## Features
- **Save** and **load** data to/from a JSON file.
- **Supports all data types** with generic methods.
- Simple, easy-to-use implementation with error handling.

## Instructions

### 1. Adding the Script
Save the `PlayerDataSaver` code in a file called `PlayerDataSaver.cs` inside the **`Scripts`** folder of your Unity project.  
Ensure that you have the `Newtonsoft.Json` package installed via NuGet or Unity Package Manager.

### 2. Creating a Data Class
Create a class that holds the data you want to save. For example:

```csharp
[Serializable] // IMPORTANT for serialization
public class PlayerData
{
    public string Name;
    public int Level;
    public int Score;
}
```

### 3. Saving Data
Use the `PlayerDataSaver.Save()` method to save an instance of your data class:

```csharp
void SaveGame()
{
    PlayerData player = new PlayerData
    {
        Name = "Fox",
        Level = 5,
        Score = 1500
    };

    PlayerDataSaver.Save(player);
    Debug.Log("Data saved!");
}
```

### 4. Loading Data
Use `PlayerDataSaver.Load<T>()` to load saved data:

```csharp
void LoadGame()
{
    if (File.Exists(Application.persistentDataPath + "/SavePlayerData.json"))
    {
        PlayerData player = PlayerDataSaver.Load<PlayerData>();
        Debug.Log($"Loaded: {player.Name}, Level: {player.Level}, Score: {player.Score}");
    }
    else
    {
        Debug.LogWarning("No saved data found!");
    }
}
```

### 5. Where Are the Data Stored?
The file **`SavePlayerData.json`** is saved in `Application.persistentDataPath`:

- **Windows**: `C:\Users\YOUR_USER\AppData\LocalLow\YourCompanyName\YourGameName`
- **Mac**: `~/Library/Application Support/YourCompanyName/YourGameName`
- **Android**: `/data/data/com.yourgame.name/files/`

### 6. Running in Unity
Create a simple `GameManager.cs` to test saving and loading:

```csharp
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        SaveGame();
        LoadGame();
    }

    void SaveGame()
    {
        PlayerData player = new PlayerData { Name = "Fox", Level = 5, Score = 2000 };
        PlayerDataSaver.Save(player);
    }

    void LoadGame()
    {
        PlayerData player = PlayerDataSaver.Load<PlayerData>();
        Debug.Log($"Loaded: {player.Name}, Level: {player.Level}, Score: {player.Score}");
    }
}
```
