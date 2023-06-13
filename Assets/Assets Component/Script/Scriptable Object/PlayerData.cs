using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "ScriptableObject/Entities/Player Data", order = 0)]
public class PlayerData : ScriptableObject
{
    public string playerName;
    [field: SerializeField] public float PlayerSpeed { get; private set; }
}