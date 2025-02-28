using TMPro;
using UnityEngine;
using UnityEngine.UI;


// Missing: item stacking, Visual representation of weapons and armor,
public class Inventory_Manager : MonoBehaviour
{
    // Singleton pattern implementation for the inventory manager
    public static Inventory_Manager instance;

    // Delegates for inventory events
    public delegate void InventoryDelegate(); // Delegate for void functions
    public InventoryDelegate EquipWeaponEvent; // Triggers reassessment of usable weapons
    public InventoryDelegate EquipArmorEvent; // Updates modifiers in character stats

    [Header("Player References")]
    [SerializeField] Transform player; // Reference to the player's transform
    [SerializeField] Keybinds KEY;

    [Header("Inventory References")]
    public Inventory inventory; // Reference to the inventory ScriptableObject
    [SerializeField] private GameObject inventoryUI; // Reference to the inventory UI GameObject
    [SerializeField] private Transform inventoryItemsField; // Parent transform for inventory item UI elements

    [Header("Item References")]
    [SerializeField] private GameObject itemUIprefab; // Prefab for displaying items in the UI

    [Header("Item UI Elements")]
    [SerializeField] private GameObject selectWeaponPanel; // Panel for selecting which weapon slot to replace
    [SerializeField] private GameObject infoPanel; // Panel for displaying detailed item information
    [SerializeField] private Image infoSprite; // Image component for displaying the item's sprite
    [SerializeField] private TMP_Text itemName; // Text component for displaying the item's name
    [SerializeField] private TMP_Text itemDiscriptionField; // Text component for displaying the item's description

    [Header("UI Buttons Elements")]
    [SerializeField] private Button equipWeaponBTN; // Button for equipping a weapon
    [SerializeField] private Button equipArmorBTN; // Button for equipping armor
    [SerializeField] private Button consumeItemBTN; // Button for consuming an item

    private Item itemRef; // Reference to the currently selected item

    private void Awake()
    {
        // Singleton pattern implementation
       /* if (instance != null && instance != this)
        {
            Debug.LogWarning($"Inventory manager destroyed (duplicate): {gameObject.name}");
            Destroy(gameObject);
            return;
        }*/

        instance = this;
        DontDestroyOnLoad(gameObject); // Makes the inventory manager persist across scenes
    }

    private void Start()
    {
        // Check if the inventory is assigned
        if (inventory == null)
        {
            Debug.LogWarning("Inventory is not assigned!");
        }
        // Assign inventory UI to UI manager (placeholder for future implementation)
    }
    private void Update()
    {
        if (Input.GetKeyDown(KEY.Inventory))
        {
            if (inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(false);
                return;
            }
            inventoryUI.SetActive(inventoryUI.activeSelf);
        }
    }

    /// <summary>
    /// Adds an item to the inventory and updates the UI.
    /// </summary>
    /// <param name="newItem">The item to add to the inventory.</param>
    public void AddItem(Item newItem)
    {
        if (newItem != null)
        {
            inventory.allItemsList.Add(newItem);
            Debug.Log($"Added {newItem.itemName} to inventory.");
            UpdateInventoryUI(); // Refresh the inventory UI
        }
        else
        {
            Debug.LogWarning("Tried to add a null item to inventory.");
        }
    }

    /// <summary>
    /// Removes an item from the inventory and updates the UI.
    /// </summary>
    /// <param name="itemToRemove">The item to remove from the inventory.</param>
    public void RemoveItem(Item itemToRemove)
    {
        if (!HasItem(itemToRemove))
        {
            Debug.LogWarning("Tried to remove an item that is not in inventory.");
            return;
        }

        inventory.allItemsList.Remove(itemToRemove);
        UpdateInventoryUI(); // Refresh the inventory UI
        Debug.Log($"Removed {itemToRemove.itemName} from inventory.");
    }

    /// <summary>
    /// Checks if the inventory contains a specific item.
    /// </summary>
    /// <param name="item">The item to check for.</param>
    /// <returns>True if the item is in the inventory, otherwise false.</returns>
    public bool HasItem(Item item)
    {
        return inventory.allItemsList.Contains(item);
    }

    /// <summary>
    /// Displays all items in the inventory (for debugging purposes).
    /// </summary>
    public void PrintInventory()
    {
        Debug.Log("Inventory contains:");
        foreach (var item in inventory.allItemsList)
        {
            Debug.Log($"- {item.itemName}");
        }
    }

    /// <summary>
    /// Equips armor to the player (placeholder implementation).
    /// </summary>
    /// <param name="armor">The armor item to equip.</param>
    public void EquipArmor(Item armor)
    {
        Debug.Log("Equiping armor");
    }

    /// <summary>
    /// Equips a weapon to the specified slot.
    /// </summary>
    /// <param name="index">The index of the weapon slot (0, 1, or 2).</param>
    public void EquipWeapon(int index)
    {
        // Clamp the index to ensure it's within the valid range (0, 1, or 2)
        Mathf.Clamp(index, 0, 2);

        // Ensure the index is within the valid range before proceeding
        if (index < 0 || index >= inventory.weaponsArray.Length)
        {
            Debug.LogWarning($"Invalid weapon index: {index}. Must be between 0 and {inventory.weaponsArray.Length - 1}.");
            return;
        }

        // Safety check for null weapon reference
        if (itemRef == null)
        {
            Debug.LogWarning("itemRef is null! Cannot replace the weapon.");
            return;
        }

        // Add the currently equipped weapon back to the inventory
        if (inventory.weaponsArray[index] != null)
        {
            AddItem(inventory.weaponsArray[index]);
        }

        // Equip the new weapon
        inventory.weaponsArray[index] = itemRef;
        RemoveItem(itemRef); // Remove the equipped item from the inventory

        EquipWeaponEvent?.Invoke(); // Trigger the equip weapon event
        HideEquipButtons(); // Hide the equip buttons
        selectWeaponPanel.SetActive(false); // Close the weapon selection panel
    }

    /// <summary>
    /// Uses a consumable item (placeholder implementation).
    /// </summary>
    public void UseConsumable()
    {
        Debug.Log("Using consumable");
    }

    /// <summary>
    /// Updates the inventory UI by clearing and repopulating it with items.
    /// </summary>
    void UpdateInventoryUI()
    {
        // Clear existing UI elements
        foreach (Transform child in inventoryItemsField)
        {
            Destroy(child.gameObject);
        }

        // Add new UI elements for each item in the inventory
        foreach (var item in inventory.allItemsList)
        {
            GameObject newItem = Instantiate(itemUIprefab, inventoryItemsField);
            newItem.GetComponent<ItemUI>().AsignItem(item); // Assign the item to the UI element
        }
    }

    /// <summary>
    /// Displays detailed information about the selected item in the info panel.
    /// </summary>
    /// <param name="itemInfo">The item to display information for.</param>
    public void ItemInfo(Item itemInfo)
    {
        if (itemRef != itemInfo)
        {
            itemRef = itemInfo;
            infoPanel.SetActive(true); // Show the info panel
            itemName.text = itemRef.itemName; // Set the item name
            itemDiscriptionField.text = itemRef.itemDescription; // Set the item description
            infoSprite.sprite = itemRef.itemSprite; // Set the item sprite
        }

        // Show the appropriate button based on the item type
        switch (itemRef.whatIsItem)
        {
            case Item.WhatIsItem.Weapon:
                equipWeaponBTN.gameObject.SetActive(true); // Show the equip weapon button
                return;
            case Item.WhatIsItem.Armor:
                equipArmorBTN.gameObject.SetActive(true); // Show the equip armor button
                return;
            case Item.WhatIsItem.Consumable:
                consumeItemBTN.gameObject.SetActive(true); // Show the consume item button
                return;
        }
    }

    /// <summary>
    /// Closes the item info panel and hides the equip buttons.
    /// </summary>
    public void CloseInfo()
    {
        infoPanel.SetActive(false); // Hide the info panel
        HideEquipButtons(); // Hide the equip buttons
    }

    /// <summary>
    /// Drops the selected item from the inventory and instantiates it in the game world.
    /// </summary>
    public void DropItem()
    {
        inventory.allItemsList.Remove(itemRef); // Remove the item from the inventory
        Instantiate(itemRef.itemPrefab, player); // Instantiate the item in the game world
    }

    /// <summary>
    /// Hides the equip weapon and equip armor buttons.
    /// </summary>
    void HideEquipButtons()
    {
        if (equipArmorBTN.gameObject.activeSelf || equipWeaponBTN.gameObject.activeSelf)
        {
            equipWeaponBTN.gameObject.SetActive(false); // Hide the equip weapon button
            equipArmorBTN.gameObject.SetActive(false); // Hide the equip armor button
        }
    }
}