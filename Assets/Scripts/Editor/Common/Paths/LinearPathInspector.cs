///
// Reference: http://catlikecoding.com/unity/tutorials/lines-and-lines/
///

using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// Provide a visualization for linear paths within the editor view.
[CustomEditor(typeof(LinearPath))]
public class LinearPathInspector : Editor
{
    private LinearPath m_path;
    private Transform m_handleTransform;
    private Quaternion m_handleRotation;

    private const float m_handleSize = 0.04f;
    private const float m_pickSize = 0.06f;
    private int m_selectedIndex = -1;

    private void OnSceneGUI()
    {
        m_path = target as LinearPath;

        if(m_path.ControlPointCount > 0)
        {
            // Transform the path into WorldSpace.
            m_handleTransform = m_path.transform;
            // Get the rotation mode.
            m_handleRotation = Tools.pivotRotation == PivotRotation.Local ?
                m_handleTransform.rotation : Quaternion.identity;

            Vector3 p0 = ShowPoint(0);
            for (int i = 1; i < m_path.ControlPointCount; ++i)
            {
                // Create a handle for each point.
                Vector3 p1 = ShowPoint(i);

                // Draw the lines between handles.
                Handles.color = Color.white;
                Handles.DrawLine(p0, p1);

                p0 = p1;
            }
        }
    }

    // Allow the points to be dragged individually.
    private Vector3 ShowPoint(int index)
    {
        Vector3 point = m_handleTransform.TransformPoint(m_path.GetControlPoint(index));

        float size = HandleUtility.GetHandleSize(point); // Keep the handle size constant regardless of view.
        if(index == 0)
        {
            size *= 2f; // Make the first point larger than the rest.
        }

        // Show Button Handles for each point.
        if (Handles.Button(point, m_handleRotation, size * m_handleSize, size * m_pickSize, Handles.DotCap))
        {
            m_selectedIndex = index;
            Repaint(); // Force the Inspector GUI to refresh.
        }
        // Show the Transform Handle for the selected point.
        if(m_selectedIndex == index)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, m_handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                // Enable Undo for this action.
                Undo.RecordObject(m_path, "Move Point");
                // Let Unity know that a change was made.
                EditorUtility.SetDirty(m_path);
                // Update the path's point position in localspace.
                m_path.SetControlPoint(index, m_handleTransform.InverseTransformPoint(point));
            }
        }
        return point;
    }

    public override void OnInspectorGUI()
    {
        m_path = target as LinearPath;

        EditorGUI.BeginChangeCheck();
        bool loop = EditorGUILayout.Toggle("Loop", m_path.Loop);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_path, "Toggle Loop");
            EditorUtility.SetDirty(m_path);
            m_path.Loop = loop;
        }

        // Do we have a point selected?
        if (m_selectedIndex >= 0 && m_selectedIndex < m_path.ControlPointCount)
        {
            DrawSelectedPointInspector();
        }
        else
        {
            // Add and remove only affect the end of the path.
            if (GUILayout.Button("Add Point"))
            {
                Undo.RecordObject(m_path, "Add Point");
                m_path.AddPoint(m_path.ControlPointCount - 1);
                EditorUtility.SetDirty(m_path);
            }

            if (m_path.ControlPointCount > 0)
            {
                if (GUILayout.Button("Remove Point"))
                {
                    Undo.RecordObject(m_path, "Remove Point");
                    m_path.RemovePoint(m_path.ControlPointCount - 1);
                    EditorUtility.SetDirty(m_path);
                }
            }
        }
    }

    private void DrawSelectedPointInspector()
    {
        GUILayout.Label("-----Selected Point-----");

        EditorGUI.BeginChangeCheck();
        // Display the vector of the selected point in the inspector.
        Vector3 point = EditorGUILayout.Vector3Field("Position", m_path.GetControlPoint(m_selectedIndex));
        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(m_path, "Move Point");
            m_path.SetControlPoint(m_selectedIndex, point);
            EditorUtility.SetDirty(m_path);
        }

        if (GUILayout.Button("Insert Point"))
        {
            Undo.RecordObject(m_path, "Insert Point");
            m_path.AddPoint(m_selectedIndex);
            EditorUtility.SetDirty(m_path);
        }

        if (m_path.ControlPointCount > 0)
        {
            if (GUILayout.Button("Remove Selected Point"))
            {
                Undo.RecordObject(m_path, "Remove Point");
                m_path.RemovePoint(m_selectedIndex);
                EditorUtility.SetDirty(m_path);
            }
        }
    }
}
