using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        DrawGUIField(position, property, label);
        GUI.enabled = true;
    }

    protected virtual void DrawGUIField(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);
    }
}
