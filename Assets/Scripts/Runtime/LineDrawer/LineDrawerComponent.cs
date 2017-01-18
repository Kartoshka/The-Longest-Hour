using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class LineDrawerComponent : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////

	public enum State
	{
		Drawing,
		Erasing
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public BezierSpline m_bezierSplineComponent;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	public float m_newPointDistance;
	public BezierSpline.BezierControlPointMode m_curveMode;
	public LineRenderer m_lineRenderer;

	private BezierSpline m_bezierSpline;
	private int m_currentPointIndex = 0;
	private bool m_createPointFlag = true;
	private bool m_dragPointFlag = false;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{
		m_bezierSpline = m_bezierSplineComponent;
    }

	// Update is called once per frame
	void Update()
	{
		ProcessInputs();
		if(m_createPointFlag)
		{
			m_bezierSpline.AddCurve();
			m_currentPointIndex += m_bezierSpline.getDimension();

			// Start control point.
			if(m_currentPointIndex - m_bezierSpline.getDimension() > 0)
			{
				m_bezierSpline.SetControlPointMode(m_currentPointIndex - m_bezierSpline.getDimension(), m_curveMode);
			}
			else
			{
				m_bezierSpline.SetControlPointMode(m_currentPointIndex - m_bezierSpline.getDimension(), BezierSpline.BezierControlPointMode.Free);
			}
			m_bezierSpline.SetControlPoint(m_currentPointIndex - m_bezierSpline.getDimension(), transform.position);
			// End control point.
			m_bezierSpline.SetControlPoint(m_currentPointIndex, transform.position);
			m_bezierSpline.SetControlPointMode(m_currentPointIndex, BezierSpline.BezierControlPointMode.Free);
			// Tween control points.
			for (int i = 1; i < m_bezierSpline.getDimension(); ++i)
			{
				//m_bezierSpline.SetControlPointMode(m_currentPointIndex - i, BezierSpline.BezierControlPointMode.Free);
				m_bezierSpline.SetControlPoint(m_currentPointIndex - i, transform.position);
			}

			m_createPointFlag = false;
			m_dragPointFlag = true;
        }
		if(m_dragPointFlag)
		{
			int segmentStartPointIndex = m_currentPointIndex - m_bezierSpline.getDimension();
            if (segmentStartPointIndex >= 0)
			{
				Vector3 segmentStartPointPosition = m_bezierSpline.GetControlPoint(segmentStartPointIndex);
                if (Vector3.Distance(this.transform.position, segmentStartPointPosition) > m_newPointDistance)
				{
					m_dragPointFlag = false;
					m_createPointFlag = true;
				}
				else
				{
                    for (int i = m_bezierSpline.getDimension(); i >= 1; --i)
					{
						//m_bezierSpline.SetControlPointMode(m_currentPointIndex - i + 1, m_curveMode);
						Vector3 controlPointPosition = Vector3.Lerp(segmentStartPointPosition, this.transform.position, (float)i / (float)m_bezierSpline.getDimension());
						m_bezierSpline.SetControlPoint(m_currentPointIndex - m_bezierSpline.getDimension() + i, controlPointPosition);
					}
				}
			}
		}
	}

	private void ProcessInputs()
	{

	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}
