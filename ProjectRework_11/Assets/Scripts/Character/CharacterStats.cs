using UnityEngine;
[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Soulles/Character/Stats")]
public class CharacterStats : ScriptableObject
{
    [Header("---- Basic Info ----")]
    public string characterName;
    public int level;
    public int health;
    public int stamina;
    public int mana;

    [Header("Base Attributes")]
    public BaseStats baseStats;

    [Header("---- Derived Stats (Calculated)----")]

    [Header("Dex increases")]
    public float dexDamageMultiplier;
    public float lockpickingSuccessRate;
    public float fallDamageResistance;
    public float injuryResistance;

    [Header("Str increases")]
    public float strDamageMultiplier;
    public float stunResistance;
    public float damageResistance;
    public float blockDamageMultiplier;

    [Header("Agi increases")]
    public float staminaMultiplier;
    public float staminaRegenRate;
    public float runSpeedMultiplier;// if u run the speed slowly increses to a max just like in nier automata

    [Header("int increases")]
    public float manaMultiplier;
    public float manaRegenRate;
    public float magicDamageMultiplier;
    public float specializedMagicMultiplier;
    public float spellLearningMultipier;

    [Header("Sense increases")]
    public float hearingNoiseRange;
    public float hearingNoiseMultiplier;
    public int identifyItemLevel;
    public int dangerSense; // when player is nere danger it will be mentione, when accepting quests or stuff in game if it is a setup or an equvelent how good can ur character tell you if he has a bad fealing

    [Header("luck increases")]
    public float goodLootRate;
    public float chanceToOvercome;// can 0vercome poisonong or injurs statuseffect

    [Header("Faith increases")]
    public int resistancToMentalAttacks;
    public float holyPowerMultiplier;
    public float reputationMultiplier;

    [Header("Arcane increases")]
    public float darkMagicMultiplier;
    public float illusionResistance;
    public float curseResistance;
    public float unluckyEventRate;// rate of witch you will inconter monsters or beings that a re 20Lv above you.


    public void CalculateDerivedStats()
    {
        //basic stuff
        health = level * 5 + baseStats.vitality * 5 + 100;
        stamina = level * 2 + baseStats.agility * 2 + 50;
        mana = level + baseStats.intelligence * 4 + 20;

        // Dexterity-based stats
        dexDamageMultiplier = 1.0f + (baseStats.dexterity * 0.05f);
        lockpickingSuccessRate = baseStats.dexterity * 2;
        fallDamageResistance = baseStats.dexterity * 0.1f;
        injuryResistance = baseStats.dexterity * 0.15f;

        // Strength-based stats
        strDamageMultiplier = 1.0f + (baseStats.strength * 0.07f);
        stunResistance = baseStats.strength * 0.5f;
        damageResistance = baseStats.strength * 3 + 5 * level;
        blockDamageMultiplier = 1.0f + (baseStats.strength * 0.03f);

        // Agility-based stats
        staminaMultiplier = 1.0f + (baseStats.agility * 0.04f);
        staminaRegenRate = baseStats.agility * 0.2f;
        runSpeedMultiplier = 1.0f + (baseStats.agility * 0.01f);

        // Intelligence-based stats
        manaMultiplier = 1.0f + (baseStats.intelligence * 0.06f);
        manaRegenRate = baseStats.intelligence * 0.3f;
        magicDamageMultiplier = 1.0f + (baseStats.intelligence * 0.05f);

        // Sense-based stats
        hearingNoiseRange = 1.0f + (baseStats.sense * 0.01f);
        hearingNoiseMultiplier = 1.0f + (baseStats.sense * 0.02f);
        identifyItemLevel = baseStats.sense / 2;
        dangerSense = baseStats.sense * 2;

        // Luck-based stats
        goodLootRate = baseStats.luck * 2;
        chanceToOvercome = baseStats.luck * 0.05f;

        // Faith-based stats
        holyPowerMultiplier = baseStats.faith * 2;
        reputationMultiplier =baseStats.faith * 0.05f;

        // Arcane-based stats
        darkMagicMultiplier = baseStats.arcane * 2;
        illusionResistance = baseStats.arcane * 0.5f;
        curseResistance = baseStats.arcane * 3;
        unluckyEventRate = 1.0f / (1 + baseStats.arcane * 0.02f);
    }
}
