///
// Reference: http://catlikecoding.com/unity/tutorials/Splines-and-splines/
///

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// Provide a visualization for lines within the editor view.
[CustomEditor(typeof(BezierSpline))]
public class BezierSplineInspector : Editor
{
    private BezierSpline spline;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private const int stepsPerCurve = 10;
    private const float directionScale = 1.75f;
    private const float bezierWidth = 5f;
    private const Bezier.FunctionType function = Bezier.FunctionType.CUBIC;

    private const float handleSize = 0.04f;
    private const float pickSize = 0.06f;
    private int selectedIndex = -1;

	private bool drawDirections = false;	//TODO: Make this a toggle on the component's inspector.

    private void OnSceneGUI()
    {
        spline = target as BezierSpline;

        // Transform line into WorldSpace.
        handleTransform = spline.transform;
        // Get the rotation mode.
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        for (int i = 1; i < spline.ControlPointCount; i += 3)
        {
            // Create a handle for each point.
            Vector3 p1 = ShowPoint(i);
            Vector3 p2 = ShowPoint(i + 1);
            Vector3 p3 = ShowPoint(i + 2);

            // Draw the lines between handles.
            Handles.color = Color.gray;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p2, p3);

            // Draw the bezier spline.
            Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
            p0 = p3;
        }
		if(drawDirections)
		{
			ShowDirections();
		}
    }

    private void ShowDirections()
    {
        Handles.color = Color.green;
        Vector3 lineStart = spline.GetPoint(0f);

        // Draw each spline segment.
        Handles.DrawLine(lineStart, lineStart + spline.GetDirection(0f) * directionScale);
        int steps = stepsPerCurve * Bezier.GetCurveSegmentCount(spline.ControlPointCount, function);
        for (int i = 1, n = steps; i < n; ++i)
        {
            lineStart = spline.GetPoint(i / (float)n);
            Handles.DrawLine(lineStart, lineStart + spline.GetDirection(i / (float)n) * directionScale);
        }
    }

    private static Color[] modeColors = {
        Color.white,
        Color.yellow,
        Color.cyan
    };

    // Allow the points to be dragged individually.
    private Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(spline.GetControlPoint(index));

        float size = HandleUtility.GetHandleSize(point); // Keep the handle size constant regardless of view.
        if(index == 0)
        {
            size *= 2f; // Make the first point larger than the rest.
        }
        Handles.color = modeColors[(int)spline.GetControlPointMode(index)];
        // Show Button Handles for each point.
        if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotCap))
        {
            selectedIndex = index;
            Repaint(); // Force the Inspector GUI to refresh.
        }
        // Show the Transform Handle for the selected point.
        if(selectedIndex == index)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                // Enable Undo for this action.
                Undo.RecordObject(spline, "Move Point");
                // Let Unity know that a change was made.
                EditorUtility.SetDirty(spline);
                // Update the spline's point position in localspace.
                spline.SetControlPoint(index, handleTransform.InverseTransformPoint(point));
            }
        }
        return point;
    }

    public override void OnInspectorGUI()
    {
        spline = target as BezierSpline;

        EditorGUI.BeginChangeCheck();
        bool loop = EditorGUILayout.Toggle("Loop", spline.Loop);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "Toggle Loop");
            EditorUtility.SetDirty(spline);
            spline.Loop = loop;
        }

        if (selectedIndex >= 0 && selectedIndex < spline.ControlPointCount)
        {
            DrawSelectedPointInspector();
        }

        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(spline, "Add Curve");
            spline.AddCurve();
            EditorUtility.SetDirty(spline);
        }

        if (GUILayout.Button("Remove Curve"))
        {
            Undo.RecordObject(spline, "Remove Curve");
            spline.RemoveCurve();
            EditorUtility.SetDirty(spline);
        }
    }

    private void DrawSelectedPointInspector()
    {
        GUILayout.Label("Selected Point");

        EditorGUI.BeginChangeCheck();
        // Display the vector of the selected point in the inspector.
        Vector3 point = EditorGUILayout.Vector3Field("Position", spline.GetControlPoint(selectedIndex));
        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "Move Point");
            spline.SetControlPoint(selectedIndex, point);
            EditorUtility.SetDirty(spline);
        }

        EditorGUI.BeginChangeCheck();
        // Display the mode of the selected point in the inspector.
        BezierSpline.BezierControlPointMode mode = (BezierSpline.BezierControlPointMode)
            EditorGUILayout.EnumPopup("Mode", spline.GetControlPointMode(selectedIndex));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "Change Point Mode");
            spline.SetControlPointMode(selectedIndex, mode);
            EditorUtility.SetDirty(spline);
        }
    }
}
