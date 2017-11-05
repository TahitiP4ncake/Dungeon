using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomGenerator))]
public class LevelGenerator : Editor {

   

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RoomGenerator generator = (RoomGenerator)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Generate Room"))
        {
            generator.DrawRoom();
        }

        if (GUILayout.Button("Clean Room"))
        {
            generator.EraseRoom();
        }

        GUILayout.EndHorizontal();
    }
}
