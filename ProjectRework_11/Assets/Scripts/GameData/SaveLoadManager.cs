using UnityEngine;
using System.IO;
using System.Linq;

public class SaveLoadManager : MonoBehaviour
{
    private string saveDirectory;

    private void Awake()
    {
        saveDirectory = Application.persistentDataPath + "/Saves/";
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
            Debug.Log("[SaveLoadManager] Save directory created at: " + saveDirectory);
        }
    }

    public void SaveStats(string saveName)
    {
        CharacterStats stats = FindObjectOfType<CharacterStats>();

        if (stats == null)
        {
            Debug.LogError("[SaveLoadManager] Save failed! CharacterStats component is missing.");
            return;
        }

        string json = JsonUtility.ToJson(stats, true);
        string filePath = saveDirectory + saveName + ".json";

        try
        {
            File.WriteAllText(filePath, json);
            Debug.Log("[SaveLoadManager] Successfully saved stats to: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("[SaveLoadManager] Error saving file: " + e.Message);
        }
    }

    public void LoadStats(string saveName)
    {
        string filePath = saveDirectory + saveName + ".json";

        if (!File.Exists(filePath))
        {
            Debug.LogError("[SaveLoadManager] Load failed! Save file not found: " + filePath);
            return;
        }

        string json = File.ReadAllText(filePath);
        CharacterStats stats = FindObjectOfType<CharacterStats>();

        if (stats == null)
        {
            Debug.LogError("[SaveLoadManager] Load failed! CharacterStats component is missing.");
            return;
        }

        try
        {
            JsonUtility.FromJsonOverwrite(json, stats);
            Debug.Log("[SaveLoadManager] Successfully loaded stats from: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("[SaveLoadManager] Error loading file: " + e.Message);
        }
    }

    public string[] GetSaveFiles()
    {
        if (!Directory.Exists(saveDirectory))
        {
            Debug.LogWarning("[SaveLoadManager] Save directory not found!");
            return new string[0];
        }

        string[] files = Directory.GetFiles(saveDirectory, "*.json") // Get all save files
                                 .Select(Path.GetFileNameWithoutExtension) // Remove .json extension
                                 .ToArray();

        return files.Length > 0 ? files : new string[0]; // Return files or empty array
    }

    // Returns the latest save file based on the last write time
    public string GetLatestSaveFile()
    {
        if (!Directory.Exists(saveDirectory))
        {
            Debug.LogWarning("[SaveLoadManager] Save directory not found!");
            return null;
        }

        var saveFiles = Directory.GetFiles(saveDirectory, "*.json");
        if (saveFiles.Length == 0)
        {
            Debug.LogWarning("[SaveLoadManager] No save files found!");
            return null;
        }

        // Get the most recently modified save file
        string latestSave = saveFiles.OrderByDescending(File.GetLastWriteTime).First();
        return Path.GetFileNameWithoutExtension(latestSave); // Return file name without extension
    }

    // Example function to create a new save file
    public void CreateNewSave(string saveName, string saveData)
    {
        string filePath = saveDirectory + saveName + ".json";
        File.WriteAllText(filePath, saveData);
        Debug.Log("[SaveLoadManager] Save created: " + filePath);
    }

    // Example function to load a save file
    public string LoadSave(string saveName)
    {
        string filePath = saveDirectory + saveName + ".json";
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        else
        {
            Debug.LogWarning("[SaveLoadManager] Save file not found: " + saveName);
            return null;
        }
    }
}

