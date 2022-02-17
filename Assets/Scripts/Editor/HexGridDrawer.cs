using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[CustomEditor(typeof(HexGrid))]
public class HexGridDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        HexGrid hexGrid = (HexGrid)target;
        if (GUILayout.Button("Generate Terrain"))
        {
            hexGrid.RebuildTerrain();
        }
    }
}
