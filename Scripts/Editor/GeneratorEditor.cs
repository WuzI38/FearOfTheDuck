# if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
// The purpose of this script is to create a custom button
// in the inspector that calls GenerateDungeon method when pressed

// Set the second parameter to true, so child classes will
// also show this editor
[CustomEditor(typeof(AbstractDungeonGenerator), true)]

public class GeneratorEditor : Editor
{
    AbstractDungeonGenerator generator;

    private void Awake() {
        generator = (AbstractDungeonGenerator)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon")) {
            generator.GenerateDungeon();
        }
    }
}
# endif