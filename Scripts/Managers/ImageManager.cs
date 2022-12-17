using UnityEngine;
using UnityEngine.Tilemaps;
using Constants;

public class ImageManager : Singleton<ImageManager>
{
    // This class stores references to all of the images and tile objects
    // And enables getting them

    [SerializeField]
    private TileBase floorTile;

    [SerializeField]
    private TileBase wallTile;

    [SerializeField]
    private TileBase spikeTile0;
    [SerializeField]
    private TileBase spikeTile1;
    [SerializeField]
    private TileBase spikeTile2;
    [SerializeField]
    private TileBase spikeTile3;

    [SerializeField]
    private TileBase startTile;

    [SerializeField]
    private TileBase exitTile;

    [SerializeField]
    private Sprite healthSmall;

    [SerializeField]
    private Sprite pistol;

    [SerializeField]
    private Sprite rifle;

    public TileBase GetTile(tileType tileName) {
        TileBase tile = floorTile;
        switch(tileName) {
            case tileType.floor:
            default:
                break;
            case tileType.wall:
                tile = wallTile;
                break;
            case tileType.start:
                tile = startTile;
                break;
            case tileType.exit:
                tile = exitTile;
                break;
            case tileType.spike_0:
                tile = spikeTile0;
                break;
            case tileType.spike_1:
                tile = spikeTile1;
                break;
            case tileType.spike_2:
                tile = spikeTile2;
                break;
            case tileType.spike_3:
                tile = spikeTile3;
                break;
        }
        return tile;
    }

    public Sprite GetSprite(int spriteName) {
        Sprite sprite = healthSmall;
        switch(spriteName) {
            case 0:
            default:
                break;
            case 1:
                sprite = pistol;
                break;
            case 2:
                sprite = rifle;
                break;
        }
        return sprite;
    }
}
