using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CorridorBase : RandomWalkGenerator
{
    // Variable roomSquarePercent is used to calculate some funky formula I've used' to eliminate those
    // very long dungeons (cause they are boring pffff) 
    [SerializeField]
    private CorridorGenParams corridorParams;

    protected override void Generate() {
        CorridorBaseGeneration();
    }

    // This is the main generator, it creates the whole dungeon so... I guess it is pretty important
    private void CorridorBaseGeneration() {
        // Store corridor ends + positions of floor tiles
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        List<Vector2Int> roomPositions = new List<Vector2Int>();
        HashSet<Vector2Int> spikePositions = new HashSet<Vector2Int>();
    
        bool flag = false;
        int funkyFormula = (int)Mathf.Ceil(Mathf.Sqrt((float)corridorParams.roomCount / corridorParams.roomSquarePercent));

        // This SHOULD result in more dense dungeons?
        int protection = 0;
        do {
            deadEnds = CreateCorridors(floorPos, potentialRoomPos); 
            // Protect the game from infinite loops
            if(protection > 1000) {
                Debug.Log("Maximum iterations achieved");
                break;
            }
            // Iterate (well... that is a lot of iteration) through all dead ends to check if
            // the distance between furthest rooms is greater than funkyFormula
            // If so repeat generation
            int minX = -1;
            int maxX = 1;
            int minY = -1;
            int maxY = 1;
            foreach(Vector2Int end in potentialRoomPos) {
                if(end.x > maxX) maxX = end.x;
                if(end.y > maxY) maxY = end.y;
                if(end.x < minX) minX = end.x;
                if(end.y < minY) minY = end.y;
            }
            // If between rooms there is more than funkyFormula * corridorLen space repeat generation
            int maxDist = Mathf.Max((maxX - minX) / corridorParams.corridorLen, (maxY - minY) / corridorParams.corridorLen);
            // Debug.Log(maxDist);
            if(maxDist >= funkyFormula) {
                floorPos.Clear();
                potentialRoomPos.Clear();
                flag = true;
            }
            else flag = false;
            protection++;
        } while(flag);

        // Find all the dead ends in our dungeon after creating corridors
        
        // Make sure, there are some rooms generated at the end of each of the dead ends
        // Save room positions to a list
        roomPositions = ChooseRoomPositions(potentialRoomPos, deadEnds);

        // Replace floor tiles with spike tiles next to rooms
        spikePositions = SpikeGenerator.CreateSpikes(roomPositions, floorPos, visualizer, corridorParams.corridorLen);

        // Generate each separate room on given positions
        HashSet<Vector2Int> roomTiles = CreateRooms(roomPositions);

        // Join room positions and corridor positions into one collection
        floorPos.UnionWith(roomTiles);

        // Create spawn and exit tiles (position saved before during corridor generation)
        StartAndExitPicker.CreateStartAndExit(floorPos, visualizer);

        // Visualize the dungeon
        visualizer.CreateFloorTiles(floorPos);
        WallGenerator.CreateWalls(floorPos, visualizer, spikePositions);
    }

    // Create full corridor layout
    private List<Vector2Int> CreateCorridors(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> potentialRoomPos) {
        Vector2Int currentPos = startPos;
        potentialRoomPos.Add(currentPos);
        // First position (by default (0, 0)) is the start position
        StartAndExitPicker.StartPos = currentPos;

        // Creates corridorCount corridors (may overlap)
        // Increase the number of corridors if
        // the number of rooms is too small
        int i = 0;
        while (potentialRoomPos.Count < corridorParams.roomCount || i < corridorParams.corridorCount) {
            List<Vector2Int> path = GenerationAlgorithms.RandomWalkCorridor(currentPos, corridorParams.corridorLen);
            currentPos = path[path.Count - 1];
            potentialRoomPos.Add(currentPos);
            floorPos.UnionWith(path);
            i++;
            // After generating all of the corridors pick the last chosen position for an exit
            // Every iteration chceck if chosen position is not the same as the start position
            if(currentPos != StartAndExitPicker.StartPos) {
                StartAndExitPicker.ExitPos = currentPos;
            }
        }

        // It is way easier to find death ends before making corridors wider
        // Because in the process of making corridors wider blocks with only one adjacent block disappear
        List<Vector2Int> deadEnds = FindDeadEnds(potentialRoomPos, floorPos);

        HashSet<Vector2Int> toAdd = new HashSet<Vector2Int>();
        // After generating the corridors make them 2 tiles wide
        foreach(Vector2Int corPos in floorPos) {
            // If there is an element on the right, add an element on the top
            if(floorPos.Contains(corPos + new Vector2Int(1, 0))) {
                toAdd.Add(corPos + new Vector2Int(0, 1));
            }
            // If there is an element on the top, add an element on the right
            else if(floorPos.Contains(corPos + new Vector2Int(0, 1))) {
                toAdd.Add(corPos + new Vector2Int(1, 0));
            }
            // Else this thing is the left curve, so it is necessary to add 3 blocks
            else {
                toAdd.Add(corPos + new Vector2Int(1, 0));
                toAdd.Add(corPos + new Vector2Int(0, 1));
                toAdd.Add(corPos + new Vector2Int(1, 1));
            }
        }   
        floorPos.UnionWith(toAdd);

        return deadEnds;
    }

    // Prevents the game from generating dead ends, doesn't work if corridor count is huge and roomCount is small
    private List<Vector2Int> FindDeadEnds(HashSet<Vector2Int> potentialRoomPos, HashSet<Vector2Int> floorPos) {
        // Find corridor tiles without neighbours
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach(Vector2Int potPos in potentialRoomPos) {
            if(Helpers.Direction2D.CountAdjacentTiles(potPos, floorPos) < 2) {
                deadEnds.Add(potPos);
                // Debug.Log("Dead end found");
            }
        }
        return deadEnds;
    }

    private List<Vector2Int> ChooseRoomPositions(HashSet<Vector2Int> potentialRoomPos, List<Vector2Int> deadEnds) {
        // Generate roomCount rooms
        int toCreateNum = corridorParams.roomCount;

        // Guid is item identifier, so choosing the order of the elements by its automatically 
        // Assigned Id is almost like choosing a subset of random values
        List<Vector2Int> toCreate = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(toCreateNum).ToList();

        // After choosing the rooms' positions make sure, that the dead ends' positions are included
        IEnumerable<Vector2Int> intersection = toCreate.Intersect(deadEnds);
        if(intersection.Any()) {
            // Remove elements included in both lists
            foreach(Vector2Int inter in intersection) {
                deadEnds.Remove(inter);
            }
            foreach(Vector2Int dead in deadEnds) {
                toCreate.Add(dead);
            }
        }
        // If there is no exit room create one (I promise, it is the last if statement)
        if(!toCreate.Contains(StartAndExitPicker.ExitPos)) {
            toCreate.Add(StartAndExitPicker.ExitPos);
        }

        // Now... let's chceck 2 things... Is there a room in startPos? And is startpos next to exitpos?
        int index = 0;
        while(!toCreate.Contains(StartAndExitPicker.StartPos) || 
            (Mathf.Abs(StartAndExitPicker.StartPos.x - StartAndExitPicker.ExitPos.x) <= corridorParams.corridorLen &&
            Mathf.Abs(StartAndExitPicker.StartPos.y - StartAndExitPicker.ExitPos.y) <= corridorParams.corridorLen)) {
                StartAndExitPicker.StartPos = toCreate[index];
                index++;
        }

        return toCreate;
    }

    // Create room at the end of all corridors (using deadEnds removal)
    private HashSet<Vector2Int> CreateRooms(List<Vector2Int> toCreate) {
        HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();

        
        
        foreach (Vector2Int pos in toCreate) {
            HashSet<Vector2Int> floorPos = RunRandomWalk(randomWalkParams, pos, corridorParams.corridorLen);
            // Positions of all of the room tiles
            roomPos.UnionWith(floorPos);
        }

        // Make rooms a bit prettier
        Normalize(roomPos);
        FillGaps(roomPos);

        return roomPos;
    }
}