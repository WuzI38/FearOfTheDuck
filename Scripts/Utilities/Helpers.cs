using UnityEngine;
using System.Collections.Generic;

// Define some useful methods
namespace Helpers
{ 
    public static class Utils {
        // While removing an object, destroy all of its children
        public static void DestroyChildren(this Transform t) {
            foreach (Transform child in t) Object.Destroy(child.gameObject);
        }

        // Calculates formula for MaxRoomSize, so I don't have to remember it
        public static int CalculateMaxRoomSize(int corridorLen) {
            return corridorLen / 2 - 4;
        }
    }

    public static class Direction2D {
        private static List<Vector2Int> DirectionList = new List<Vector2Int> {
            new Vector2Int(0, 1), // Up
            new Vector2Int(1, 0), // Right
            new Vector2Int(0, -1), // Down
            new Vector2Int(-1, 0) // Left
        };

        private static List<Vector2Int> DirectionListExtended = new List<Vector2Int> {
            new Vector2Int(0, 1), // Up
            new Vector2Int(1, 0), // Right
            new Vector2Int(0, -1), // Down
            new Vector2Int(-1, 0), // Left
            new Vector2Int(1, 1), // Up + right
            new Vector2Int(-1, 1), // Up + left
            new Vector2Int(1, -1), // Down + right
            new Vector2Int(-1, -1) // Down + left
        };

        public static List<Vector2Int> GetDirectionList() {
            return DirectionList;
        }

        public static List<Vector2Int> GetDirectionListExtended() {
            return DirectionListExtended;
        }

        // Return a random direction
        public static Vector2Int GetRandomDirection() {
            return DirectionList[Random.Range(0, DirectionList.Count)];
        }

        // Find all possible positions adjacent to floor tiles, where walls can be placed
        public static HashSet<Vector2Int> FindAdjacentWalls(HashSet<Vector2Int> floorPos) {
            HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
            foreach(Vector2Int pos in floorPos) {
                foreach(Vector2Int dir in DirectionListExtended) {
                    Vector2Int adjacent = pos + dir;
                    if(!floorPos.Contains(adjacent)) {
                        wallPos.Add(adjacent);
                    }
                }
            }
            return wallPos;
        }

        // Count all the floor tiles adjacent to the given floor tile
        public static int CountAdjacentTiles(Vector2Int tilePos, HashSet<Vector2Int> objPos) {
            int neighbours = 0;
            foreach(Vector2Int dir in DirectionList) {
                Vector2Int adjacent = tilePos + dir;
                if(objPos.Contains(adjacent)) {
                    neighbours++;
                }
            }
            return neighbours;
        }
    }
}

// Get player's velocity (I don't know, maybe calculating positions will be easier thanks to that)
//Vector2 playerSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity;
