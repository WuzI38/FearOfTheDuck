using UnityEngine;
using UnityEditor;

public class PlayerSpawner : MonoBehaviour
{
    GameObject player;
    [SerializeField] 
    private GunScriptable gunParams;
    public void SpawnPlayer(Vector2Int position) {
        // Instantiate player object on given position
        string path = "Assets/Prefabs/Player.prefab";
        GameObject playerPrefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
        float size = playerPrefab.GetComponent<SpriteRenderer>().size.x / 2;
        player = Instantiate(playerPrefab, new Vector3(size + position.x * size * 2, size + position.y * size * 2, 0), Quaternion.identity);
        SetStartingInventory();
    }

    void SetStartingInventory() {
        // Add basic rifle to player's starting inventory
        Player playerScript = player.GetComponent<Player>();
        Gun gun = new Rifle(playerScript, gunParams);
        playerScript.AddToPlayerInventory(gun);
    }
}