using UnityEngine;

[CreateAssetMenu(fileName = "CorridorGen", menuName = "Sriptables/CorridorGenData")]
public class CorridorGenParams : ScriptableBase {
    public int corridorLen = 20;
    public int corridorCount = 20;
    public int roomCount = 11;
    public float roomSquarePercent = 0.65f;
}
