///
// Reference: http://catlikecoding.com/unity/tutorials/curves-and-splines/
///

using UnityEditor;
using UnityEngine;

// Provide a visualization for lines within the editor view.
[CustomEditor(typeof(Line))]
public class LineInspector : Editor
{
    private void OnSceneGUI()
    {
        Line line = target as Line;

        // Transform line into WorldSpace.
        Transform handleTransform = line.transform;
        Vector3 p0 = handleTransform.TransformPoint(line.p0);
        Vector3 p1 = handleTransform.TransformPoint(line.p1);
        // Get the rotation mode.
        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;

        // Create a line.
        Handles.color = Color.white;
        Handles.DrawLine(p0, p1);

        // Create handles for both points on the line.
        Handles.DoPositionHandle(p0, handleRotation);
        Handles.DoPositionHandle(p1, handleRotation);

        // Allow the points to be dragged individually.
        EditorGUI.BeginChangeCheck();
        p0 = Handles.DoPositionHandle(p0, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            // Enable Undo for this action.
            Undo.RecordObject(line, "Move Point");
            // Let Unity know that a change was made.
            EditorUtility.SetDirty(line);
            // Update the line's endpoint position in localspace.
            line.p0 = handleTransform.InverseTransformPoint(p0);
        }
        EditorGUI.BeginChangeCheck();
        p1 = Handles.DoPositionHandle(p1, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(line, "Move Point");
            EditorUtility.SetDirty(line);
            line.p1 = handleTransform.InverseTransformPoint(p1);
        }
    }
}
