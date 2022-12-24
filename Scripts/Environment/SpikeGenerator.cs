using System.Collections.Generic;
using UnityEngine;

public static class SpikeGenerator
{
    private static HashSet<Vector2Int> spikePos = new HashSet<Vector2Int>();
    public static HashSet<Vector2Int> SpikePositions {
        get {
            return spikePos.Count > 0 ? spikePos : null;
        }
    }
    // Create spikes blocking the path if there are remaining enemies in the room
    public static HashSet<Vector2Int> CreateSpikes(List<Vector2Int> roomPos, HashSet<Vector2Int> floorPos, TilemapVisualizer visualizer, int corridorLen) {
        int roomSize = Helpers.Utils.CalculateMaxRoomSize(corridorLen);

        // Iterate through all room positions searching for corridors that can contain spikes
        foreach(Vector2Int room in roomPos) {
            foreach(Vector2Int dir in Helpers.Direction2D.GetDirectionList()) {
                Vector2Int potentialSpikePos = room + (dir * (roomSize + 2));
                if(floorPos.Contains(potentialSpikePos)) {
                    floorPos.Remove(potentialSpikePos);
                    spikePos.Add(potentialSpikePos);
                    visualizer.CreateSpike(potentialSpikePos);
                    // Corridor's axis is left / right - add a spike above
                    Vector2Int newPos;
                    if(dir.x != 0) {   
                        newPos = potentialSpikePos + new Vector2Int(0, 1);
                        floorPos.Remove(newPos);
                        spikePos.Add(newPos);
                    }
                    // Corridor's axis is top / bottom - add a spike on the right
                    else {
                        newPos = potentialSpikePos + new Vector2Int(1, 0);
                        floorPos.Remove(newPos);
                        spikePos.Add(newPos);
                    }
                    visualizer.CreateSpike(newPos);
                }
            }
        }
        return spikePos;
    }
}
