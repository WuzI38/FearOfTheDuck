using System.Collections.Generic;
using UnityEngine;

public static class StartAndExitPicker
{
    // Getters and seters created to set positions during generation without risk
    private static Vector2Int startPos = Vector2Int.zero;
    public static Vector2Int StartPos {
        get => startPos;
        set {
            startPos = value;
        }
    }
    private static Vector2Int exitPos = Vector2Int.zero;

    public static Vector2Int ExitPos {
        get => exitPos;
        set {
            exitPos = value;
        }
    }

    public static HashSet<Vector2Int> CreateStartAndExit(HashSet<Vector2Int> floorPos, TilemapVisualizer visualizer) {
        HashSet<Vector2Int> sae = new HashSet<Vector2Int>();
        // Visualize start and exit tiles
        if(floorPos.Contains(exitPos)) {
            floorPos.Remove(exitPos);
            visualizer.CreateStart(exitPos);
            sae.Add(exitPos);
        }
        // Create 4 spawn tiles
        Vector2Int[] points = {
            startPos, 
            startPos + new Vector2Int(0, 1), 
            startPos + new Vector2Int(-1, 0),
            startPos + new Vector2Int(-1, 1)
        };
        foreach(Vector2Int point in points) {
            if(floorPos.Contains(point)) {
                floorPos.Remove(point);
                visualizer.CreateStart(point);
                sae.Add(point);
            }
        }
        return sae;
    }
}
