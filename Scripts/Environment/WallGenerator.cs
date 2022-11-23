using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    // Create wall around the room
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer visualizer, HashSet<Vector2Int> spikes = null) {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        wallPos = Helpers.Direction2D.FindAdjacentWalls(floorPos);
        foreach(Vector2Int pos in wallPos) {
            if(spikes == null) {
                // Do not chcek if there are any spikes if it is room only generation
                visualizer.CreateWall(pos);
            }
            else {
                if(!spikes.Contains(pos)) {
                // Do not create walls under spikes, cause it makes no sense
                    visualizer.CreateWall(pos);
                }
            }
        }
    }
}