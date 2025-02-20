using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    public SaveLoadManager saveLoadManager;
    public CharacterStats characterStats;
    private string currentSaveName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("[Game_Manager] Initialized.");
        }
        else
        {
            Debug.LogWarning("[Game_Manager] Duplicate instance detected. Destroying.");
            Destroy(gameObject);
            return;
        }

        saveLoadManager = GetComponent<SaveLoadManager>();
        if (saveLoadManager == null)
        {
            Debug.LogError("[Game_Manager] SaveLoadManager is missing from the scene!");
        }

        LoadLastSave();
    }

    private void LoadLastSave()
    {
        currentSaveName = PlayerPrefs.GetString("LastSave", "");
        if (!string.IsNullOrEmpty(currentSaveName))
        {
            Debug.Log("[Game_Manager] Loading last save: " + currentSaveName);
            saveLoadManager.LoadStats(currentSaveName);
        }
        else
        {
            Debug.LogWarning("[Game_Manager] No previous save found.");
        }
    }

    public void SaveGame()
    {
        if (!string.IsNullOrEmpty(currentSaveName))
        {
            saveLoadManager.SaveStats(currentSaveName);
            Debug.Log("[Game_Manager] Game saved as: " + currentSaveName);
        }
        else
        {
            Debug.LogWarning("[Game_Manager] No save selected! Use SaveLoadUI to select a save.");
        }
    }

    public void LoadGame(string saveName)
    {
        if (!string.IsNullOrEmpty(saveName))
        {
            Debug.Log("[Game_Manager] Loading game: " + saveName);
            saveLoadManager.LoadStats(saveName);
            currentSaveName = saveName;
            PlayerPrefs.SetString("LastSave", saveName);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogError("[Game_Manager] Load failed! Save name is empty.");
        }
    }

    public void CreateNewSave(string saveName)
    {
        if (string.IsNullOrEmpty(saveName))
        {
            Debug.LogError("[Game_Manager] Cannot create save. Save name is empty!");
            return;
        }

        currentSaveName = saveName;
        SaveGame();
        Debug.Log("[Game_Manager] New save created: " + saveName);
    }
}
