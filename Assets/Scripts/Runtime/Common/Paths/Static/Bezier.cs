using UnityEngine;

// Reference: http://catlikecoding.com/unity/tutorials/curves-and-splines/
public static class Bezier
{
    public enum FunctionType
    {
        LINEAR = 1,
        QUADRATIC = 2,
        CUBIC = 3,
    }

    public static Vector3 GetPoint(Vector3[] points, float t, FunctionType function)
    {
        if (points.Length <= (int)function)
        {
            return Vector3.zero;
        }

        int i;
        GetPointAlongCurve(ref points, function, t, out t, out i);
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        switch(function)
        {
            case (FunctionType.QUADRATIC):
                {
                    return oneMinusT * oneMinusT * points[i] +
                        2f * oneMinusT * t * points[i+1] +
                        t * t * points[i+2];
                }
            case (FunctionType.CUBIC):
                {
                    return oneMinusT * oneMinusT * oneMinusT * points[i] +
                        3f * oneMinusT * oneMinusT * t * points[i+1] +
                        3f * oneMinusT * t * t * points[i+2] +
                        t * t * t * points[i+3];
                }
            default:
                return points[i];
        }
    }

    public static Vector3 GetFirstDerivative(Vector3[] points, float t, FunctionType function)
    {
        if (points.Length <= (int)function)
        {
            return Vector3.zero;
        }

        int i;
        GetPointAlongCurve(ref points, function, t, out t, out i);
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        switch (function)
        {
            case (FunctionType.QUADRATIC):
                {
                    return 2f * oneMinusT * (points[i+1] - points[i]) +
                        2f * t * (points[i+2] - points[i+1]);
                }
            case (FunctionType.CUBIC):
                {
                    return 3f* oneMinusT * oneMinusT * (points[i+1] - points[i]) +
                        6f * oneMinusT * t * (points[i+2] - points[i+1]) +
                        3f * t * t * (points[i+3] - points[i+2]);
                }
            default:
                return Vector3.zero;
        }
    }

    // Number of number of segments that make up this curve.
    public static int GetCurveSegmentCount(int controlPointCount, FunctionType function)
    {
            return (controlPointCount - 1) / (int)function;
    }

    // Get the fractional point along a curve made up of points.
    private static void GetPointAlongCurve(ref Vector3[] points, FunctionType function, float tIn, out float tOut, out int segment)
    {
        if (tIn >= 1f)
        {
            tOut = 1f;
            segment = points.Length - 1 - (int)function;
        }
        else
        {
            // If there was only 1 segment, i = 0 and t = 1 - t.
            tOut = Mathf.Clamp01(tIn) * GetCurveSegmentCount(points.Length, function);
            segment = (int)tOut;
            tOut -= segment;
            segment *= (int)function;
        }
    }
}