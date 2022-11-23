using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilemapVisualizer), true)]

public class ClearEditor : Editor
{
    TilemapVisualizer visualizer;

    private void Awake() {
        visualizer = (TilemapVisualizer)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(GUILayout.Button("Clear Dungeon")) {
            visualizer.Clear();
        }
    }
}
