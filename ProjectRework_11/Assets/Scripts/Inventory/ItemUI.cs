using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{// Reference to the Image component that displays the item's sprite
    private Image sprite;

    // Reference to the Button component that allows interaction with the item UI
    private Button button;

    // Reference to the Item scriptable object associated with this UI element
    private Item itemRef;

    // Reference to the sprite of the item (stored separately for easy access)
    private Sprite itemSprite;

    private void Start()
    {
        // Get the Button component attached to this GameObject
        button = GetComponent<Button>();

        // Add a listener to the button so that when it's clicked, the OpenItemUI method is called
        button.onClick.AddListener(OpenItemUI);

        // Get the Image component from the first child of this GameObject
        // This assumes the first child is the UI element displaying the item's sprite
        sprite = transform.GetChild(0).GetComponent<Image>();
    }

    /// <summary>
    /// Assigns an item to this UI element and updates the displayed sprite.
    /// </summary>
    /// <param name="newItem">The Item scriptable object to be assigned.</param>
    public void AsignItem(Item newItem)
    {
        // Store the reference to the new item
        itemRef = newItem;

        // Get the sprite from the item and store it
        itemSprite = itemRef.itemSprite;

        // Update the Image component to display the item's sprite
        sprite.sprite = itemSprite;
    }

    /// <summary>
    /// Opens the item info panel in the Inventory_Manager when this item's UI is clicked.
    /// </summary>
    void OpenItemUI()
    {
        // Call the ItemInfo method on the Inventory_Manager singleton instance
        // This will display the item's details in the info panel
        Inventory_Manager.instance.ItemInfo(itemRef);
    }
}
