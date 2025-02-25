using UnityEngine;
using UnityEngine.Events;

public class Game_Manager : MonoBehaviour
{
    enum GameState
    {
        Playeing,
        Paused,
        Dialogue,
        Cutscene,
        Saving,
        Loading
    }

    [Header("Game manager instance")]
    public static Game_Manager instance; // instance call of a game manager

    [Header("Game Save settings")]
    public SaveLoadManager saveLoadManager; // This will be a manager later or incoparated with a Game manager
    public CharacterStats characterStats; // character stats reference probably wount ned it 
    private string currentSaveName; // curent save we are playing

    [Header("Game Progress tracking")]
    public int progressIndex;// tracks game progress and triggers events accordingly

    [Header("Game Events")]
    public UnityEvent StartGame; // plays at the start of the game intangled with progress index
    public UnityEvent PlayerDeathEvent;// called when bool player alive is false

    [Header("Game trackers")]
    public bool playerAlive;// while player health is 100 aka when player health reaches 0 calls event and sets bool to false
    public bool sceneMainMenu; // if in main menu this bool is active
    public bool sceneVillage; // if inside a village this bool is active
    public bool sceneCity; // if inside cities this bool is active
    public bool sceneCaves; // if inside a cave this bool is active
    public bool sceneDungeon; // if in dungeons this bool is active
    public bool sceneWildernes;// if in the wildernes this bool is active

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

    public void GameStoryProgressUpdate(int progress)
    {
        // functios depending on the games progress migt be a scrip called game progress and its accesd here.
    }
}
