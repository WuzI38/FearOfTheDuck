using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    // Use serialize field to create a reference to a private object in the inspector
    protected TilemapVisualizer visualizer = null;
    protected Vector2Int startPos = Vector2Int.zero;

    public void GenerateDungeon() {
        // Set the reference to ImageHandler
        visualizer.Init();
        // Reset the dungeon to an empty tilemap if it already exists
        visualizer.Clear();
        // Invoke proper generation method
        Generate();
    }

    // This abstract method must be implemented in derivative classes
    protected abstract void Generate();
}
