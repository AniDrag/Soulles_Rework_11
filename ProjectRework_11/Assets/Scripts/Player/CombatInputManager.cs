using NaughtyAttributes;
using UnityEngine;


public class CombatInputManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;// weapon acces
    [SerializeField] private Keybinds KEY;
    WeaponEquipedIndex[] weapons = new WeaponEquipedIndex[3];
    [SerializeField] Camera playerCamera;



    int weaponIndex;
    bool weaponSwitching;
    bool bareHanded;
    // ranged weapon
    bool aimMode;

    // Ranged weapon type Bow Draw and relese. Auto, burst, single fire, grenade launcher, laser, Magic casting, cancle cast, reload, aiming with weapon toggle mode and Hold.

    private void Start()
    {
        CheckAllWeapons();
        Inventory_Manager.instance.EquipWeaponEvent += CheckAllWeapons;
    }

    private void Update()
    {
        PlayerInputs();
    }

    void PlayerInputs()
    {
        if (!weaponSwitching)
        {
            SwitchOutWeapon();

            if (weapons[weaponIndex] == null || bareHanded) return;// this is on top

            if (weapons[weaponIndex].weaponType == WeaponEquipedIndex.WeaponType.RangedWeapon)
            {
                RanggedInputs();
            }
            else
            {
                MaleInputs();
            } // else read maleInputs
        }
    }

    void RanggedInputs()
    {
        if (Input.GetKeyDown(KEY.Aim) && !aimMode)
        {
            aimMode = true;
            playerCamera.fieldOfView = 20;
            AimWeapon();
        }// aiming On
        if (Input.GetKeyUp(KEY.Aim) && aimMode)
        {
            aimMode = false;
            playerCamera.fieldOfView = 60;
        }// aimng off
    }
    void MaleInputs()
    {

    }
    [Button("Check All Weapons", enabledMode: EButtonEnableMode.Playmode)]
    void CheckAllWeapons()
    {
        for (int i = 0; i < weapons.Length; i++){
            if(inventory.weaponsArray[i] == null)
            {
                continue;
            }
            weapons[i].ItemId = inventory.weaponsArray[i].itemID;
            weapons[i].weaponPrefab = inventory.weaponsArray[i].itemPrefab;
            CheckWeaponType(weapons[i].weaponPrefab, i);
        }

    }

    // goes tghru the wapon trys to get ither component and determines what type of weapon it is
    void CheckWeaponType(GameObject weapon, int i)
    {
        if (weapon.GetComponent<RangedWeapon>())
        {
            weapons[i].weaponType = WeaponEquipedIndex.WeaponType.RangedWeapon;
            RangedWeapon rangedWeapon = weapon.GetComponent<RangedWeapon>();
        }
        else if (weapon.GetComponent<MeleWeapon>())
        {
            weapons[i].weaponType = WeaponEquipedIndex.WeaponType.MaleWeapon;
        }
        else
        {
            Debug.LogError("Weapon item invalid. Could not get component Ranged/Male Weapon");
            return;
        }
        Debug.Log($"Weapon[{i}] type = {weapons[i].weaponType}");

    }
    ////////////////////////////////////////////////////////////////////
    //------------------- Throwables / consumabels mechanics -----------
    ////////////////////////////////////////////////////////////////////
    [Button("Use consumable", enabledMode: EButtonEnableMode.Playmode)]
    private void UseConsumable()
    {

    }
    [Button("use throwable", enabledMode: EButtonEnableMode.Playmode)]
    private void UseThrowable()
    {

    }
    ////////////////////////////////////////////////////////////////////
    //------------------- Ranged weapon mechanics ----------------------
    ////////////////////////////////////////////////////////////////////
    [Button("Fire weapon", enabledMode: EButtonEnableMode.Playmode)]
    private void FireWeapon()// fire ranged weapon
    {

    }
    [Button("Aim weapon", enabledMode: EButtonEnableMode.Playmode)]
    private void AimWeapon()// aim with ranged weapon
    {
        Debug.Log("Aiming weapon");
    }
    ////////////////////////////////////////////////////////////////////
    //------------------- Mele weapon mechanics ------------------------
    ////////////////////////////////////////////////////////////////////
    [Button("Male attack", enabledMode: EButtonEnableMode.Playmode)]
    private void MaleAttack()
    {
        
    }
    [Button("Male secondary action", enabledMode: EButtonEnableMode.Playmode)]
    private void MaleSecondaryAction()
    {

    }

    ////////////////////////////////////////////////////////////////////
    //------------------- Weapon switching ------------------------
    ////////////////////////////////////////////////////////////////////
    void SwitchOutWeapon()
    {
        
        if (Input.mouseScrollDelta.y != 0)// if scroll whele isnt 0 then trigger.
        {   // weapon index = ( weapon index + ( if (mouse input > 0 then =1 else -1) + weapon lenght (3) % weapon lenght(3))
            weaponIndex = (weaponIndex + (Input.mouseScrollDelta.y > 0 ? 1 : -1) + weapons.Length) % weapons.Length;
            WeaponSwitch();
            // x =(x +( 1 if(y>0) or -1 if(y<0), + 3) % 3) Example x = -1 + 3% 3 = 2%3 = 2 This is fo me to remember
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            weaponIndex=0;
        } // weapon index = 0
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponIndex = 1;
        }// weapon index = 1
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponIndex = 2;
        }// weapon index = 2


    }
    void WeaponSwitch()
    {// play weapon switching animation, Update Ui,
        Debug.Log("Weapon equipped: " + weaponIndex);
    }
}
