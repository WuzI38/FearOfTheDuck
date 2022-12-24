using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    private static HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
    public static HashSet<Vector2Int> WallPositions {
        get {
            if(wallPositions.Count > 0) return wallPositions;
            else return null;
        }
    }
    // Create wall around the room
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer visualizer, HashSet<Vector2Int> spikes = null, HashSet<Vector2Int> sae = null) {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        wallPos = Helpers.Direction2D.FindAdjacentWalls(floorPos);
        foreach(Vector2Int pos in wallPos) {
            // Spikes exist only if start and exit (sae) exist
            if(spikes == null && sae == null) {
                // Do not chcek if there are any spikes if it is room only generation
                wallPositions.Add(pos);
                visualizer.CreateWall(pos);
            }
            else {
                if(!spikes.Contains(pos) && !sae.Contains(pos)) {
                // Do not create walls under spikes, cause it makes no sense
                    wallPositions.Add(pos);
                    visualizer.CreateWall(pos);
                }
            }
        }
    }
}