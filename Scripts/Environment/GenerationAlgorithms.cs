using System.Collections.Generic;
using UnityEngine;

public static class GenerationAlgorithms
{
    // Implementation of random walk algorithm
    public static HashSet<Vector2Int> RandomWalk(Vector2Int startPos, int walkLen)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>(); // HashSet is a dict with keys only
        path.Add(startPos);
        Vector2Int prevPos = startPos;

        // Create a random path (from a given point you can go in one of the 4 directions)
        for(int i = 0; i < walkLen; i++) {
            Vector2Int newPos = prevPos + Helpers.Direction2D.GetRandomDirection();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;
    }

    // Creates a path in a random direction
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLen) {
        List<Vector2Int> corridor = new List<Vector2Int>();
        Vector2Int direction = Helpers.Direction2D.GetRandomDirection();
        Vector2Int currentPos = startPos;
        for(int i = 0; i < corridorLen; i++) {
            currentPos += direction;
            corridor.Add(currentPos);
        }
        return corridor;
    }
}