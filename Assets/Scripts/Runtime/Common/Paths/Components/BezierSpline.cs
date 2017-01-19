using UnityEngine;
using System;

// Reference: http://catlikecoding.com/unity/tutorials/curves-and-splines/
public class BezierSpline : MonoBehaviour
{
    [SerializeField]
    private Vector3[] points;

    public int ControlPointCount
    {
        get
        {
            return points.Length;
        }
    }

    public enum BezierControlPointMode
    {
        Free,
        Aligned,
        Mirrored
    }

    [SerializeField]
    private BezierControlPointMode[] modes;

    [SerializeField]
    private bool loop;

    public bool Loop
    {
        get
        {
            return loop;
        }
        set
        {
            loop = value;
            if (value == true)
            {
                modes[modes.Length - 1] = modes[0];
                SetControlPoint(0, points[0]);
            }
        }
    }

    // One less the number of control handles.
    private int curveSegments = (int)Bezier.FunctionType.CUBIC;

	public int getDimension() { return curveSegments; }

    // Called by the editor when the component is created or reset.
    public void Reset()
    {
        points = new Vector3[]
        {
			new Vector3(1f, 0f, 0f)
			//, new Vector3(2f, 0f, 0f)
			//, new Vector3(3f, 0f, 0f)
			//, new Vector3(4f, 0f, 0f)
		};

        modes = new BezierControlPointMode[]
        {
            BezierControlPointMode.Free,
            BezierControlPointMode.Free
        };
    }

    public Vector3 GetPoint(float t)
    {
        return transform.TransformPoint(Bezier.GetPoint(points, t, Bezier.FunctionType.CUBIC));
    }

    public Vector3 GetVelocity(float t)
    {
        return transform.TransformPoint(Bezier.GetFirstDerivative(points, t, Bezier.FunctionType.CUBIC)) - transform.position;
    }

    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }

    public void AddCurve()
    {
        Vector3 point = points[points.Length - 1];
        Array.Resize(ref points, points.Length + curveSegments);
        for(int i = 0; i < curveSegments; ++i)
        {
            point.x += 1f;
            points[points.Length - curveSegments + i] = point;
        }

        Array.Resize(ref modes, modes.Length + 1);
        modes[modes.Length - 1] = modes[modes.Length - 2];
        // Enforce the constraints at the point where the curve is added.
        EnforceMode(points.Length - (curveSegments + 1));

        if (loop)
        {
            points[points.Length - 1] = points[0];
            modes[modes.Length - 1] = modes[0];
            EnforceMode(0);
        }
    }

    public void RemoveCurve()
    { 
        if(points.Length > curveSegments)
        {
            Array.Resize(ref points, Mathf.Max(0, points.Length - curveSegments));
        }
    }

    public Vector3 GetControlPoint(int index)
    {
        return points[index];
    }

    public void SetControlPoint(int index, Vector3 point)
    {
        // Ensure that if the middle control point connecting two
        // curves is moved, both surrounding control points move too.
        if (index % 3 == 0)
        {
            Vector3 delta = point - points[index];
            if (loop)
            {
                if (index == 0)
                {
                    points[points.Length - 1] = point;
                    points[1] += delta;
                    points[points.Length - 2] += delta;
                }
                else if (index == points.Length - 1)
                {
                    points[0] = point;
                    points[1] += delta;
                    points[index - 1] += delta;
                }
                else
                {
                    points[index - 1] += delta;
                    points[index + 1] += delta;
                }
            }
            else
            {
                if (index > 0)
                {
                    points[index - 1] += delta;
                }
                if (index + 1 < points.Length)
                {
                    points[index + 1] += delta;
                }
            }
        }

        points[index] = point;
        EnforceMode(index);
    }

    public BezierControlPointMode GetControlPointMode(int index)
    {
        return modes[(index + 1) / curveSegments];
    }

    public void SetControlPointMode(int index, BezierControlPointMode mode)
    {
        int modeIndex = (index + 1) / curveSegments;
        modes[modeIndex] = mode;
        if(loop)
        {
            // Ensure the endpoints' modes match.
            if(modeIndex == 0)
            {
                modes[modes.Length - 1] = mode;
            }
            else if(modeIndex == modes.Length - 1)
            {
                modes[0] = mode;
            }
        }
        EnforceMode(index);
    }

    private void EnforceMode(int index)
    {
        int modeIndex = (index + 1) / curveSegments;
        BezierControlPointMode mode = modes[modeIndex];
        // Ignore 'free' or 'end' points.
        if (mode == BezierControlPointMode.Free 
            || !loop && (modeIndex == 0 || modeIndex == modes.Length - 1))
        {
            return;
        }

        int middleIndex = modeIndex * 3; // The point connecting both curves.
        int fixedIndex; // The point we are moving.
        int enforcedIndex; // The other point, which is reacting.
        if (index <= middleIndex)
        {
            fixedIndex = middleIndex - 1;
            if (fixedIndex < 0)
            {
                fixedIndex = points.Length - 2;
            }
            enforcedIndex = middleIndex + 1;
            if (enforcedIndex >= points.Length)
            {
                enforcedIndex = 1;
            }
        }
        else
        {
            fixedIndex = middleIndex + 1;
            if (fixedIndex >= points.Length)
            {
                fixedIndex = 1;
            }
            enforcedIndex = middleIndex - 1;
            if (enforcedIndex < 0)
            {
                enforcedIndex = points.Length - 2;
            }
        }

        Vector3 middle = points[middleIndex];
        Vector3 enforcedTangent = middle - points[fixedIndex];
        if (mode == BezierControlPointMode.Aligned)
        {
            enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, points[enforcedIndex]);
        }
        points[enforcedIndex] = middle + enforcedTangent;
    }
}
