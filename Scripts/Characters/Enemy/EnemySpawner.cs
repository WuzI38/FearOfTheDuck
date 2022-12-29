using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private List<Vector2Int> spawnPositions;
    private bool initialized;
    private GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        spawnPositions = new List<Vector2Int>();
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
            // Cannot use spawnPositions.Skip(1) as spawnPositions might be reduced to just one element
            if(spawnPositions.Count > 0) {
                float size = player.GetComponent<SpriteRenderer>().size.x / 2;
                Vector2 worldPosition;
                float distance = 100f; // Just a random high value 
                foreach(Vector2Int pos in spawnPositions) {
                    // Calculate the real distance (beased on tile size)
                    worldPosition = new Vector3(size + pos.x * size * 2, size + pos.y * size * 2, 0);
                    distance = Mathf.Min(Vector2.Distance(player.transform.position, worldPosition), distance);
                    GameState state = GameManager.Instance.state;
                    // If player entered the room   
                    if(state == GameState.Running && distance < 7.45f) {
                        // Remove visited room from rooms meant to spawn enemies
                        spawnPositions.Remove(pos);
                        GameManager.Instance.ChangeState(GameState.EnterRoom);
                        // Spawn enemies
                        int toSpawn = Objective.GetNumberOfEnemiesToSpawn();
                        // Enemies have the same size as player, so I can pass player size as enemy size to calculate positions
                        SpawnEnemies(toSpawn, pos, size);
                        break;
                    }
                }
            }
        }
    }

    public List<Vector2Int> SetSpawnPositions(List<Vector2Int> allPositions, HashSet<Vector2Int> saePositions, HashSet<Vector2Int> chestPositions) {
        // Save positions not chosen as spawn, exit or chest positions to hashset
        foreach(Vector2Int pos in allPositions) {
            if(!saePositions.Contains(pos) && !chestPositions.Contains(pos)) {
                spawnPositions.Add(pos);
            }
        }
        initialized = true;
        return spawnPositions;
    }

    void SpawnEnemies(int amount, Vector2Int pos, float size) {
        List<Vector2Int> enemyPositions = new List<Vector2Int>();
        // Get the positions of walls to prevent the game from spawning enemies inside the walls
        HashSet<Vector2Int> walls = WallGenerator.WallPositions;
        for(int i = 0; i < amount; i++) {
            string path = "Prefabs/DuckDuckGo";
            GameObject enemyPrefab = Resources.Load(path, typeof(GameObject)) as GameObject;
            Vector2Int newPos;
            // Choose random position for the enemy and make sure it is not inside the wall + all chosen positions are different
            do {
                int px = Random.Range(-4, 5);
                int py = Random.Range(-4, 5);
                newPos = pos + new Vector2Int(px, py);
            } while(walls.Contains(newPos) || enemyPositions.Contains(newPos));
            GameObject enemy = GameObject.Instantiate(enemyPrefab, new Vector3(size + newPos.x * size * 2, size + newPos.y * size * 2, 0), Quaternion.identity);
        }
    }
}