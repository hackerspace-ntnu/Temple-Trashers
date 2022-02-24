using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(HexGrid))]
public class HexGridDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Terrain"))
            ((HexGrid)target).RebuildTerrain();
    }
}
