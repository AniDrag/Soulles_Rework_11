using System;
using UnityEngine;
using UnityEngine.Events;

public class MyClasses : MonoBehaviour
{ }
public class WeaponEquippedIndex
{
    public enum WeaponType
    {
        Male,
        Ranged
    }
    [Tooltip("this is the item id")]
    public int itemID;
    public GameObject weaponPrefab;
    public Animator weaponAnimation;
    public WeaponType weaponType;

}

[Serializable]
public class BaseStats
{
    public int vitality;
    public int agility;
    public int intelligence;
    public int strength;
    public int dexterity;
    public int sense;
    public int faith;
    public int arcane;
    public int luck;

    public BaseStats(int vit, int agi, int intel, int str, int dex, int sen, int fai, int arc, int luc)
    {
        vitality = vit;
        agility = agi;
        intelligence = intel;
        strength = str;
        dexterity = dex;
        sense = sen;
        faith = fai;
        arcane = arc;
        luck = luc;
    }
}

[Serializable]
public class QuestObjective
{
    // is inside Quest as a container for objectives has tipes with polymorphysom
}
[Serializable]
public class KillObjective : QuestObjective
{

}
[Serializable]
public class SpecialItemObjective : QuestObjective
{

}
[Serializable]
public class GatheringObjective : QuestObjective
{

}
[Serializable]
public class Dialogue
{

}
[Serializable]
public class DialogueOption
{

}
[Serializable]
public class DialogueEvent
{
    // enum then show a certian thing like
    // triger sometig
    // aquire resources
    // trugger a quest
    // shit like that 
    public UnityEvent triggerAction; 
}
    
