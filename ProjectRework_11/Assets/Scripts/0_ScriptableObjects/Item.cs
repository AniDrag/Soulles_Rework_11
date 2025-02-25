using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Soulless /Item")]
public class Item : ScriptableObject
{
    public enum WhatIsItem
    {
        Resource,
        Consumable,
        Weapon,
        Armor,
        SpecialItem,
        Misc // Changed from "Onject"
    }

    [Tooltip("Item name. Displayed in inventory or when looting.")]
    public string itemName;

    [Tooltip("Short description of the item.")]
    [TextArea]
    public string itemDescription;

    [Tooltip("The physical object prefab of this item, used when equipping or spawning.")]
    public GameObject itemPrefab; // Renamed from itemObject

    [Tooltip("Icon for the item when displayed in the inventory.")]
    public Sprite itemSprite; // Changed from Image to Sprite

    [Tooltip("ID of item, used for identifying and calling the item.")]
    public int itemID;

    [Tooltip("Classifies the type of item (Resource, Consumable, etc.).")]
    public WhatIsItem whatIsItem;

}

