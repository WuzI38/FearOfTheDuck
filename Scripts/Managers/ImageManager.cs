using UnityEngine;
using UnityEngine.Tilemaps;

public class ImageManager : Singleton<ImageManager>
{
    // This class stores references to all of the images and tile objects
    // And enables getting them

    [SerializeField]
    private TileBase floorTile;

    [SerializeField]
    private TileBase wallTile;

    [SerializeField]
    private TileBase spikeTile;

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

    public TileBase GetTile(int tileName) {
        TileBase tile = floorTile;
        switch(tileName) {
            case 0:
            default:
                break;
            case 1:
                tile = wallTile;
                break;
            case 2:
                tile = startTile;
                break;
            case 3:
                tile = exitTile;
                break;
            case 4:
                tile = spikeTile;
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
