using UnityEngine;

[CreateAssetMenu(fileName = "Keybinds", menuName = "Soulless/Keybinds")]
public class Keybinds : ScriptableObject
{
    public KeyCode Aim = KeyCode.Mouse1;
    public KeyCode Cast = KeyCode.Mouse1;
    public KeyCode Inventory = KeyCode.Tab;

}
