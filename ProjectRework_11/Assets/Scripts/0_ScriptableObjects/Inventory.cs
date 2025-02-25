using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Soulless/Inventory")]
public class Inventory : ScriptableObject
{
    [Header("Inventory items")]
    public List<Item> allItemsList = new List<Item>();

    [Header("Gear slots")]
    public Item helmet;
    public Item chestArmor;
    public Item legArmor;
    public Item boots;
    public Item[] ringArray = new Item[3]; // Max 3 rings
    public Item necklace;

    [Header("Weapon slots")]
    public Item[] weaponsArray = new Item[3]; // More flexible

    [Header("Consumable slots")]
    public Item[] consumablesArray = new Item[3]; // Easier to manage
}
