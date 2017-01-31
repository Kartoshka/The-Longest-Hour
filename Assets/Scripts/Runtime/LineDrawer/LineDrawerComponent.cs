﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class LineDrawerComponent : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////

	public enum DrawState
	{
		Uncreated,
		Adding,
		Following,
		Complete
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public BezierSpline m_bezierSplineComponent; // TODO: Remove this to get rid of duplication.
//	public string m_splineComponentTag;

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

	public ParticleSystem m_particles;

	public float m_completionDistanceCheck;
	public LineWalker m_lineWalker;

	public TimeAngle m_timeAngle; //TODO: Remove this. We just want an easy way to reset on start.

	private BezierSpline m_bezierSpline;
	private DrawState m_currentState = DrawState.Uncreated;
	private int m_splinePointCount = 0;
	private float m_closedLoopStartPoint = 0;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public float getClosedLoopStartPoint() { return m_closedLoopStartPoint; }
	public Vector3 getClosedLoopStartPosition() { return m_bezierSpline.GetPoint(m_closedLoopStartPoint); }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	private void reset()
	{
		if (m_bezierSpline)
		{
			m_bezierSpline.Reset();
			m_splinePointCount = 0;
        }

		if (m_particles)
		{
			m_particles.Stop();
			m_particles.Clear();
        }

		if(m_lineWalker)
		{
			m_lineWalker.stop();
		}

		m_currentState = DrawState.Uncreated;
    }

	private bool checkIsComplete()
	{
		bool isComplete = false;

        float checkUntilIndex = m_splinePointCount - (m_bezierSpline.getDimension() * 4);
		if(m_splinePointCount > checkUntilIndex)
		{
			for (int i = 0; i < checkUntilIndex; ++i)
			{
				if (Vector3.Distance(this.transform.position, m_bezierSpline.GetControlPoint(i)) < m_completionDistanceCheck)
				{
					isComplete = true;
					m_closedLoopStartPoint = (float)i / (float)m_splinePointCount;
                }
			}
		}

		return isComplete;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{
		m_bezierSpline = m_bezierSplineComponent;
		//if(!m_bezierSpline)
		//{
		//	GameObject splineObject = GameObject.FindGameObjectWithTag(m_splineComponentTag);
		//	if(splineObject)
		//	{
		//		m_bezierSpline = splineObject.GetComponent<BezierSpline>();
		//	}
		//}
		reset();

		// TODO: Remove this once we have a cleaner way to reset this.
		if (m_timeAngle)
		{
			m_timeAngle.reset();
		}
	}

	// Update is called once per frame
	void Update()
	{
		ProcessInputs();
		switch(m_currentState)
		{
			case (DrawState.Uncreated):
			{
				m_bezierSpline.SetControlPoint(m_splinePointCount, transform.position);
				break;
			}
			case (DrawState.Adding):
			{
				if(m_particles)
				{
					m_particles.Play();
                }

                m_bezierSpline.AddCurve();
				m_splinePointCount += m_bezierSpline.getDimension();

				// Start control point.
				if (m_splinePointCount - m_bezierSpline.getDimension() > 0)
				{
					m_bezierSpline.SetControlPointMode(m_splinePointCount - m_bezierSpline.getDimension(), m_curveMode);
				}
				else
				{
					m_bezierSpline.SetControlPointMode(m_splinePointCount - m_bezierSpline.getDimension(), BezierSpline.BezierControlPointMode.Free);
				}
				m_bezierSpline.SetControlPoint(m_splinePointCount - m_bezierSpline.getDimension(), transform.position);
				// End control point.
				m_bezierSpline.SetControlPoint(m_splinePointCount, transform.position);
				m_bezierSpline.SetControlPointMode(m_splinePointCount, BezierSpline.BezierControlPointMode.Free);
				// Tween control points.
				for (int i = 1; i < m_bezierSpline.getDimension(); ++i)
				{
					//m_bezierSpline.SetControlPointMode(m_splinePointCount - i, BezierSpline.BezierControlPointMode.Free);
					m_bezierSpline.SetControlPoint(m_splinePointCount - i, transform.position);
				}

				m_currentState = DrawState.Following;
                break;
			}
			case (DrawState.Following):
			{
				int segmentStartPointIndex = m_splinePointCount - m_bezierSpline.getDimension();
				if (segmentStartPointIndex >= 0)
				{
					Vector3 segmentStartPointPosition = m_bezierSpline.GetControlPoint(segmentStartPointIndex);
					if (Vector3.Distance(this.transform.position, segmentStartPointPosition) > m_newPointDistance)
					{
						m_currentState = DrawState.Adding;
					}
					else
					{
						for (int i = m_bezierSpline.getDimension(); i >= 1; --i)
						{
							//m_bezierSpline.SetControlPointMode(m_splinePointCount - i + 1, m_curveMode);
							Vector3 controlPointPosition = Vector3.Lerp(segmentStartPointPosition, this.transform.position, (float)i / (float)m_bezierSpline.getDimension());
							m_bezierSpline.SetControlPoint(m_splinePointCount - m_bezierSpline.getDimension() + i, controlPointPosition);
						}
					}
				}

				if (checkIsComplete())
				{
					// TODO: Remove this once we have a cleaner way to reset this.
					if (m_timeAngle)
					{
						m_timeAngle.reset();
					}

					m_currentState = DrawState.Complete;
				}
				break;
			}
			case (DrawState.Complete):
			{
				m_particles.Stop();

				if(m_lineWalker && !m_lineWalker.getIsActive())
				{
					m_lineWalker.setSpline(m_bezierSplineComponent);
                    m_lineWalker.setStartPoint(m_closedLoopStartPoint);
					m_lineWalker.reset();
					m_lineWalker.play();
				}
				break;
			}
		}
	}

	private void ProcessInputs()
	{
		if (m_currentState == DrawState.Uncreated)
		{
			if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
			{
				m_currentState = DrawState.Adding;
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
		{
			reset();
			m_currentState = DrawState.Uncreated;
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}
