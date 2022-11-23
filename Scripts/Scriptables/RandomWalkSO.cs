using UnityEngine;

[CreateAssetMenu(fileName = "RandomWalk", menuName = "Sriptables/RandomWalkData")]
public class RandomWalkSO : ScriptableBase {
    public int iterations = 10;
    public int walkLen = 10;
    public bool randomStart = false;
}
