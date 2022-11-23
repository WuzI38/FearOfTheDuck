using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class RandomWalkGenerator : AbstractDungeonGenerator
{
    // Set parameters of the generator
    [SerializeField] // Enable setting private variables from inspector
    protected RandomWalkSO randomWalkParams;

    protected override void Generate() {
        // Generate a hashset containing positions of all the tiles
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParams, startPos);

        // Normalize floor
        Normalize(floorPositions);

        // Fill the gaps
        FillGaps(floorPositions);

        // Visualize the tiles
        visualizer.CreateFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, visualizer);
    }

    // Invokes RunRandomWalk from separate file
    protected HashSet<Vector2Int> RunRandomWalk(RandomWalkSO randomWalkParams, Vector2Int pos, int corridorLen = -1) {
        Vector2Int position = pos;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        // For each iteration append new random path to floorPos
        for(int i = 0; i < randomWalkParams.iterations; i++) {
            HashSet<Vector2Int> path = GenerationAlgorithms.RandomWalk(position, randomWalkParams.walkLen);
            floorPos.UnionWith(path); // Add everything from the path to floorPos
            if(randomWalkParams.randomStart) {
                // Choose a random element
                position = floorPos.ElementAt(Random.Range(0, floorPos.Count));
            }
        }

        // Make sure there will be space for the spikes to generate
        // (works only for rooms + corridors generation, not for one room only generation)
        if(corridorLen > 0) {
            reduceMaxSize(floorPos, pos, corridorLen);
        }

        return floorPos;
    }

    // Remove most of the narrow spaces 
    protected void Normalize(HashSet<Vector2Int> floorPos) {
        // Apply recursive deletion to all floor tiles with < 2 neighbours
        HashSet<Vector2Int> delPositions = new HashSet<Vector2Int>();
        foreach(Vector2Int pos in floorPos) {
            if(Helpers.Direction2D.CountAdjacentTiles(pos, floorPos) < 2) {
                delPositions.Add(pos);
            }
        }
        foreach(Vector2Int pos in delPositions) {
            RecDel(pos, floorPos);
        }
    }

    // Recursive deletion function 
    // This thing was created, so those random 1-tile-wide passages appear less frequently
    private void RecDel(Vector2Int delPos, HashSet<Vector2Int> floorPos) {
        floorPos.Remove(delPos);
        foreach(Vector2Int dir in Helpers.Direction2D.GetDirectionList()) {
            Vector2Int newPos = dir + delPos;
            if(floorPos.Contains(newPos)) {
                if(Helpers.Direction2D.CountAdjacentTiles(newPos, floorPos) < 2) {
                    RecDel(newPos, floorPos);
                }
            }
        }
    }

    // Removes most of the small gaps (like holes of size 1x1) and replaces them with floor tiles
    // Doesn't work sometimes, no idea why, but I guess I'll take it
    protected void FillGaps(HashSet<Vector2Int> floorPos) {
        HashSet<Vector2Int> newTiles = new HashSet<Vector2Int>();
        HashSet<Vector2Int> holePos = new HashSet<Vector2Int>();
        foreach(Vector2Int pos in floorPos) {
            foreach(Vector2Int dir in Helpers.Direction2D.GetDirectionList()) {
                Vector2Int adjacent = pos + dir;
                if(!floorPos.Contains(adjacent)) {
                    if(Helpers.Direction2D.CountAdjacentTiles(adjacent, floorPos) > 2) {
                        newTiles.Add(adjacent);
                    }
                }
            }
        }
        floorPos.UnionWith(newTiles);
    }

    // Leave some open space between rooms
    protected void reduceMaxSize(HashSet<Vector2Int> floorPos, Vector2Int center, int corridorLen) {
        HashSet<Vector2Int> toRemove = new HashSet<Vector2Int>();
        foreach(Vector2Int tilePos in floorPos) {
            int maxSize = Helpers.Utils.CalculateMaxRoomSize(corridorLen);
            if(Mathf.Abs(tilePos.x - center.x) > maxSize || Mathf.Abs(tilePos.y - center.y) > maxSize) {
                toRemove.Add(tilePos);
            }
        }
        foreach(Vector2Int delPos in toRemove) {
            floorPos.Remove(delPos);
        }
    }
}
