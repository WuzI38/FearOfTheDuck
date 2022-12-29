using System.Collections.Generic;
using UnityEngine;

public static class ChestGenerator
{
    private static HashSet<Vector2Int> chestPos;
    public static HashSet<Vector2Int> ChestPositions {
        get => chestPos;
    }
    private static HashSet<GameObject> chestList = null;
    public static HashSet<Vector2Int> GenerateChests(int chests, List<Vector2Int> roomPos, HashSet<Vector2Int> startExitPos) {
        // Pick random positions for each of x chests (num of chests given as parameter)
        // Make sure those positions are not identical and are not start nor exit positions as it makes no sense
        chestPos = new HashSet<Vector2Int>();
        chestList = new HashSet<GameObject>();
        if(chests > roomPos.Count - startExitPos.Count) return null;
        while(chests > 0) {
            Vector2Int pos = roomPos[Random.Range(0, roomPos.Count)];
            if(!startExitPos.Contains(pos) && !chestPos.Contains(pos)) {
                chestPos.Add(pos);
                chests--;
            }
        }
        string path = "Prefabs/Chest";
        GameObject chestPrefab = Resources.Load(path, typeof(GameObject)) as GameObject;
        float size = chestPrefab.GetComponent<SpriteRenderer>().size.x / 2;
        foreach(Vector2Int cp in chestPos) {
            // Vector2 position = Camera.main.ScreenToWorldPoint(new Vector2(cp.x, cp.y));
            GameObject chest = GameObject.Instantiate(chestPrefab, new Vector3(size + cp.x * size * 2, size + cp.y * size * 2, 0), Quaternion.identity);
            chest.AddComponent<ObjectDestroyer>();
            chestList.Add(chest);
        }

        return chestPos;
    }

    public static void DestroyChests() {
        if(chestList == null) return;
        foreach(GameObject chest in chestList) {
            chest.GetComponent<ObjectDestroyer>().Destroy();
        }
    }
}

