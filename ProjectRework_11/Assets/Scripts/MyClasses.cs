using System;
using UnityEngine;

public class MyClasses : MonoBehaviour
{ }


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
    
