using UnityEngine;
using System;
using System.Collections.Generic;

// Multiple linear line segments all connected to make a single path.
public class LinearPath : MonoBehaviour
{
    [SerializeField]
    private Vector3[] m_points;

    public int ControlPointCount
    {
        get
        {
            return m_points.Length;
        }
    }

    [SerializeField]
    private bool m_loop;

    public bool Loop
    {
        get
        {
            return m_loop;
        }
        set
        {
            m_loop = value;
            if (value == true)
            {
                SetControlPoint(m_points.Length-1, m_points[0]);
            }
        }
    }

    private static Vector3 m_newPointOffset = new Vector3(0, 0, -1);

    // Called by the editor when the component is created or reset.
    public void Reset()
    {
        m_points = new Vector3[]
        {
            new Vector3(0f, 0f, -1f),
            new Vector3(0f, 0f, -2f)
        };
    }

    // Add a point to the path following the given index.
    public void AddPoint(int index)
    {
        Vector3 point;
        if (m_points.Length == 0)
        {
            // First time adding a curve.
            point = Vector3.zero;
        }
        else
        {
            // Continue from the given point.
            point = m_points[index];
        }
        point += m_newPointOffset;

        // If we're adding to the end of the path.
        if (index == m_points.Length - 1)
        {
            Array.Resize(ref m_points, m_points.Length + 1);
            if(m_loop)
            {
                m_points[m_points.Length - 1] = m_points[0];
            }
            else
            {
                m_points[m_points.Length - 1] = point;
            }
        }
        else
        {
            List<Vector3> copy = new List<Vector3>(m_points);
            copy.Insert(index + 1, point);
            m_points = copy.ToArray();
        }
    }

    // Remove the point from the given index.
    public void RemovePoint(int index)
    {
        if (m_points.Length > 0)
        {
            // If we're removing from the end of the path.
            if(index == m_points.Length - 1)
            {
                Array.Resize(ref m_points, Mathf.Max(0, m_points.Length - 1));
            }
            else
            {
                List<Vector3> copy = new List<Vector3>(m_points);
                copy.RemoveAt(index);
                m_points = copy.ToArray();
            }
        }
    }

    public Vector3 GetControlPoint(int index)
    {
        return m_points[index];
    }

    public void SetControlPoint(int index, Vector3 point)
    {
        m_points[index] = point;

        if (m_loop)
        {
            // Ensure the endpoints modes match.
            if (index == 0)
            {
                m_points[m_points.Length - 1] = point;
            }
            else if (index == m_points.Length - 1)
            {
                m_points[0] = point;
            }
        }
    }
}
