using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableObject
{
    public string QuestTitle;
    public string QuestDescription;
    public QuestObjective[] objectives;
    /// qeust info ig this is mor of a scriptable Object

}
