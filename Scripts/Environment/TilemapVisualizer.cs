using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Constants;

public class TilemapVisualizer : Singleton<TilemapVisualizer>
{
    // IEnumerable allows readonly access to a collection, then a collection
    // that implements IEnumerable can be used with a for-each statement.
    // Invoke CreateTiles method with the arguments given above
    [SerializeField]
    private Tilemap tilemap, wallTilemap, spikeTilemap;
    private ImageManager Imanager;

    protected override void Awake() {
        base.Awake();
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        // Spikes are hidden at the beggining
        spikeTilemap.GetComponent<TilemapCollider2D>().enabled = false;
    }

    void Destroy() {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    public void Init() {
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
        spikeTilemap.ClearAllTiles();
        // VERY BAD CODING PRACTICE DO NOT DUPLICATE
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
        foreach(GameObject chest in chests) {
            try {
                chest.GetComponent<ObjectDestroyer>().Destroy();
            }
            catch {
                // Debug.Log("No chest currently in game");
            }
        }
    }

    // Paint a single wall tile
    public void CreateWall(Vector2Int position) {
        CreateTile(position, wallTilemap, Imanager.GetTile(tileType.wall));
    }

    public void CreateSpike(Vector2Int position) {
        CreateTile(position, spikeTilemap, Imanager.GetTile(tileType.spike_0));
    }

    public void CreateStart(Vector2Int position) {
        CreateTile(position, tilemap, Imanager.GetTile(tileType.start));
    }

    public void CreateExit(Vector2Int position) {
        CreateTile(position, tilemap, Imanager.GetTile(tileType.exit));
    }

    private void switchSpikes() {
        // Disable colliders, so you can move through spikes
        spikeTilemap.GetComponent<TilemapCollider2D>().enabled = !spikeTilemap.GetComponent<TilemapCollider2D>().enabled;
    }

    private void OnGameStateChanged(GameState newGameState) {
        if(newGameState == GameState.EnterRoom ||
           newGameState == GameState.ClearRoom) {
            switchSpikes();
        }
    }
}
