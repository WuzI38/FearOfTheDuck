using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private HashSet<Vector2Int> spawnPositions;
    private bool initialized;
    private GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        spawnPositions = new HashSet<Vector2Int>();
        initialized = false;
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Static class would be probably more intuitive here, but I need Inheritance from monobehaviour so the EnemySpawner class cannot be static
        if(initialized) {
            // SHOULD BE CONTINUED FROM HERE
        }
    }

    public void SetSpawnPositions(List<Vector2Int> allPositions, HashSet<Vector2Int> saePositions, HashSet<Vector2Int> chestPositions) {
        // Save positions not chosen as spawn, exit or chest positions to hashset
        foreach(Vector2Int pos in allPositions) {
            if(!saePositions.Contains(pos) && !chestPositions.Contains(pos)) {
                spawnPositions.Add(pos);
            }
        }
        initialized = true;
    }
}
