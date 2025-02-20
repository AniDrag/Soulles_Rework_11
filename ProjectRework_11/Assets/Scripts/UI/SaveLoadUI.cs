using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SaveLoadUI : MonoBehaviour
{
    public SaveLoadManager saveLoadManager;
    public Game_Manager gameManager;
    public TMP_Dropdown saveDropdown;
    public TMP_InputField saveNameInput;

    private void Start()
    {
        saveDropdown.GetComponent<TMP_Dropdown>();
        saveNameInput = transform.GetChild(0).GetComponent<TMP_InputField>();
        if (saveLoadManager == null)
        {
            Debug.LogError("[SaveLoadUI] SaveLoadManager reference is missing!");
        }

        if (gameManager == null)
        {
            Debug.LogError("[SaveLoadUI] Game_Manager reference is missing!");
        }

        RefreshSaveList();
    }

    public void RefreshSaveList()
    {
        if (saveDropdown == null)
        {
            Debug.LogError("[SaveLoadUI] saveDropdown reference is missing!");
            return;
        }

        saveDropdown.ClearOptions();
        string[] saves = saveLoadManager.GetSaveFiles();

        if (saves.Length == 0)
        {
            Debug.LogWarning("[SaveLoadUI] No save files found.");
            saveDropdown.AddOptions(new List<string> { "No Saves Found" });
            return;
        }

        saveDropdown.AddOptions(new List<string>(saves));
    }

    public void SaveGame()
    {
        if (gameManager == null)
        {
            Debug.LogError("[SaveLoadUI] Cannot save! Game_Manager is missing.");
            return;
        }

        gameManager.SaveGame();
        RefreshSaveList();
    }

    public void LoadGame()
    {
        if (saveDropdown.options.Count == 0 || saveDropdown.options[0].text == "No Saves Found")
        {
            Debug.LogWarning("[SaveLoadUI] No valid saves to load.");
            return;
        }

        string selectedSave = saveDropdown.options[saveDropdown.value].text;
        Debug.Log("[SaveLoadUI] Loading save: " + selectedSave);
        gameManager.LoadGame(selectedSave);
    }

    public void CreateNewSave()
    {
        if (saveNameInput == null)
        {
            Debug.LogError("[SaveLoadUI] saveNameInput reference is missing!");
            return;
        }

        string saveName = saveNameInput.text;

        if (string.IsNullOrEmpty(saveName))
        {
            Debug.LogError("[SaveLoadUI] Cannot create save. Save name is empty!");
            return;
        }

        Debug.Log("[SaveLoadUI] Creating new save: " + saveName);
        gameManager.CreateNewSave(saveName);
        RefreshSaveList();
    }
    public void NewGame()
    {
        Game_Manager.instance.characterStats = new CharacterStats();
    }
}
