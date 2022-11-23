using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : Singleton<TilemapVisualizer>
{
    // IEnumerable allows readonly access to a collection, then a collection
    // that implements IEnumerable can be used with a for-each statement.
    // Invoke CreateTiles method with the arguments given above
    [SerializeField]
    private Tilemap tilemap, wallTilemap;
    private ImageManager Imanager;

    enum tileType {
        floor,
        wall,
        start,
        exit,
        spike,
    }

    protected override void Awake() {
        base.Awake();
        Imanager = GameObject.FindGameObjectWithTag("ImageHandler").GetComponent<ImageManager>();
    }
    
    public void CreateFloorTiles(IEnumerable<Vector2Int> floorPos) {
        CreateTiles(floorPos, tilemap, Imanager.GetTile((int)tileType.floor));
    }

    private void CreateTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile) {
        // Create the whole tilemap (floor tiles only)
        foreach(Vector2Int pos in positions) {
            CreateTile(pos, tilemap, tile);
        }
    }

    private void CreateTile(Vector2Int position, Tilemap tilemap, TileBase tile) {
        // Translates world position to a position on a grid
        Vector3Int tilePos = new Vector3Int(position.x, position.y, 0);
        // WorldToCell translation removed cause, it was causing generator faults
        // Moves a tile to its position on a grid
        tilemap.SetTile(tilePos, tile);
    }

    // Clears all the tiles that belong to a tilemap
    public void Clear() {
        tilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    // Paint a single wall tile
    public void CreateWall(Vector2Int position) {
        CreateTile(position, wallTilemap, Imanager.GetTile((int)tileType.wall));
    }

    public void CreateSpike(Vector2Int position) {
        CreateTile(position, tilemap, Imanager.GetTile((int)tileType.spike));
    }

    public void CreateStart(Vector2Int position) {
        CreateTile(position, tilemap, Imanager.GetTile((int)tileType.start));
    }

    public void CreateExit(Vector2Int position) {
        CreateTile(position, tilemap, Imanager.GetTile((int)tileType.exit));
    }
}
