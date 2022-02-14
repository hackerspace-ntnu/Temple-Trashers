using UnityEditor;


// Provides a way for the inspector to show the value of `HexCoordinates` fields
[CustomPropertyDrawer(typeof(HexCoordinates))]
public class HexCoordinatesDrawer : TextLabelDrawer
{}
