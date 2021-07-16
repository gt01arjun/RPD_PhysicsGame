using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelDataObject", order = 1)]
public class LevelData : ScriptableObject
{
    public int CurvedPlankCounter;
    public int HalfPlankCounter;
    public int FlatPlankCounter;
}