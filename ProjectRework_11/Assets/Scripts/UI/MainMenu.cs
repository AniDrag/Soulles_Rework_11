using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// Manages the main menu, handling UI navigation, save/load functionality, and game startup.
/// Provides methods for opening panels, loading/continuing games, and quitting the application.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [Header("Panels & UI Elements")]
    [SerializeField] private Animator InfoPanel;   // Information panel animator
    [SerializeField] private Animator OptionsPanel; // Options panel animator
    [SerializeField] private Animator NewSavePanel; // New game save panel animator
    [SerializeField] private GameObject loadGamePanel; // Panel containing save game options
    private const string activate = "Active"; // Animation parameter for activating panels

    [Header("UI Elements")]
    [SerializeField] private GameObject contineuBTN; // Continue button (enabled if a save exists)
    private Button conBTN; // Button component reference for enabling/disabling
    [SerializeField] private TMP_InputField saveNameInput; // Input field for naming a new save
    [SerializeField] private TMP_Dropdown saveDropdown; // Dropdown to select saved games

    [Header("Save System")]
    [SerializeField] private SaveLoadManager saveLoadManager; // Handles saving/loading
    [SerializeField] private Game_Manager gameManager; // Manages game state
    private string selectedSave; // Stores the selected save name from the dropdown
    private bool savesActive; // Flag to track save selection state

    void Start()
    {
        if (saveLoadManager == null)
        {
            Debug.LogError("[MainMenu] SaveLoadManager is not assigned!");
        }
        if (gameManager == null)
        {
            Debug.LogError("[MainMenu] Game_Manager is not assigned!");
        }

        // Hide the load game panel at start
        loadGamePanel.SetActive(false);

        // Disable continue button until a save is selected
        conBTN = contineuBTN.GetComponent<Button>();
        conBTN.interactable = false;

        // Load available saves
        RefreshSaveList();

        // Add listener to dropdown for detecting save selection
        saveDropdown.onValueChanged.AddListener(delegate { LoadGame(); });
        RefreshSaveList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Escape key toggles panels
        {
            HandleInput();
        }
    }

    #region Panel Management
    public void OpenInfoPanel()
    {
        InfoPanel.gameObject.SetActive(true);
        InfoPanel.SetBool(activate, true);
    }

    public void CloseInfoPanel()
    {
        InfoPanel.SetBool(activate, false);
        Invoke("DelaySetActive", 2);
    }

    public void OpenOptionPanel()
    {
        OptionsPanel.gameObject.SetActive(true);
        OptionsPanel.SetBool(activate, true);
    }

    public void CloseOptionPanel()
    {
        OptionsPanel.SetBool(activate, false);
        Invoke("DelaySetActive", 2);
    }

    void DelaySetActive()
    {
        InfoPanel.gameObject.SetActive(false);
        OptionsPanel.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void HandleInput()
    {
        if (InfoPanel.gameObject.activeSelf || OptionsPanel.gameObject.activeSelf || NewSavePanel.gameObject.activeSelf)
        {
            CloseInfoPanel();
            CloseOptionPanel();
            DeactivateNewGamePanel();
            CloseLoadGamePanel();
        }
        else
        {
            OpenOptionPanel();
        }
    }
    #endregion

    #region New Game Management
    public void ActivateNewGamePanel()
    {
        NewSavePanel.gameObject.SetActive(true);
        NewSavePanel.SetBool(activate, true);
    }

    public void DeactivateNewGamePanel()
    {
        NewSavePanel.SetBool(activate, false);
        Invoke("DelayNewGame", 1);
    }

    void DelayNewGame()
    {
        NewSavePanel.gameObject.SetActive(false);
    }

    public void NewGamePartTwo()
    {
        if (saveNameInput == null)
        {
            Debug.LogError("[MainMenu] SaveNameInput reference is missing!");
            return;
        }

        string saveName = saveNameInput.text;

        if (string.IsNullOrEmpty(saveName))
        {
            Debug.LogError("[MainMenu] Cannot create a new save. Save name is empty!");
            return;
        }

        Debug.Log("[MainMenu] Starting a new game with save name: " + saveName);
        gameManager.CreateNewSave(saveName);
        SceneManager.LoadScene("GameScene"); // Load the game scene
    }
    #endregion

    #region Load & Continue Game
    public void ContineuGame()
    {
        string latestSave = saveLoadManager.GetLatestSaveFile();
        if (latestSave != null)
        {
            Debug.Log("[MainMenu] Continuing game from latest save: " + latestSave);
            gameManager.LoadGame(latestSave);
        }
        else
        {
            Debug.LogWarning("[MainMenu] No save file found! Cannot continue.");
        }
    }

    public void LoadGame()
    {
        selectedSave = saveDropdown.options[saveDropdown.value].text;

        if (selectedSave != "No Saves Found")
        {
            conBTN.interactable = true;
            Debug.Log("[MainMenu] Selected Save: " + selectedSave);
        }
        else
        {
            conBTN.interactable = false;
        }
    }

    public void OpenLoadGamePanel()
    {
        loadGamePanel.SetActive(true);
    }

    public void CloseLoadGamePanel()
    {
        loadGamePanel.SetActive(false);
    }
    #endregion

    #region Save System
    private void RefreshSaveList()
    {
        if (saveDropdown == null)
        {
            Debug.LogError("[MainMenu] saveDropdown reference is missing!");
            return;
        }

        saveDropdown.ClearOptions();
        string[] saves = saveLoadManager.GetSaveFiles();

        if (saves.Length == 0)
        {
            Debug.Log("[MainMenu] No saves found.");
            contineuBTN.SetActive(false);
            saveDropdown.AddOptions(new List<string> { "No Saves Found" });
        }
        else
        {
            Debug.Log("[MainMenu] Found " + saves.Length + " save files.");
            contineuBTN.SetActive(true);
            saveDropdown.AddOptions(new List<string>(saves));
        }
    }
    #endregion
}