using UnityEngine;
using System.Collections;
using MOJ.Helpers;
using System.Collections.Generic;

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

    public GameObject m_particleEmitter;
	private ParticleSystem m_particles;

	public float m_completionDistanceCheck;
	public LineWalker m_lineWalker;

	public TimeShifterAimDirection m_timeAngle; //TODO: Remove this. We just want an easy way to reset on start.

    public LayerMask drawableLayers;

	private BezierSpline m_bezierSpline;
	private DrawState m_currentState = DrawState.Uncreated;
	private int m_splinePointCount = 0;
	private float m_closedLoopStartPoint = 0;

    private List<GameObject> m_encompassed = new List<GameObject>();

    private Vector3 m_raycastedPosition = new Vector3(0, 0, 0);

    public GameObject m_globalTime;

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

	public void reset()
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
				if (Vector3.Distance(m_raycastedPosition, m_bezierSpline.GetControlPoint(i)) < m_completionDistanceCheck)
				{
					isComplete = true;
					m_closedLoopStartPoint = (float)i / (float)m_splinePointCount;
                }
			}
		}

		return isComplete;
	}
    
    public void StartDrawing()
    {
        if (m_currentState == DrawState.Uncreated)
        {
            m_currentState = DrawState.Adding;
        }
    }
    
    public void ClearDrawing()
    {
        reset();

        for (int i = 0; i < m_encompassed.Count; i++)
        {
            m_encompassed[i].GetComponent<TimeControllable>().Deactivate();
        }
        m_encompassed = new List<GameObject>();

        // deactivate global time controller
        m_globalTime.GetComponent<AnimParameterController>().setActive(false);

        m_currentState = DrawState.Uncreated;
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
        m_particles = m_particleEmitter.GetComponent<ParticleSystem>();
        m_raycastedPosition = transform.position;

        m_particles.Stop();
        m_particles.Clear();
    }

	// Update is called once per frame
	void Update()
	{
		ProcessInputs();

        // calculate raycasted position
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, drawableLayers)){
            m_raycastedPosition = hit.point;
        }
        else
        {
            m_raycastedPosition = new Vector3(transform.position.x, m_raycastedPosition.y, transform.position.z);
        }

        m_particleEmitter.transform.position = new Vector3(m_raycastedPosition.x, m_raycastedPosition.y + 0.1f, m_raycastedPosition.z);

		switch(m_currentState)
		{
			case (DrawState.Uncreated):
			{
				m_bezierSpline.SetControlPoint(m_splinePointCount, m_raycastedPosition);
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
				m_bezierSpline.SetControlPoint(m_splinePointCount - m_bezierSpline.getDimension(), m_raycastedPosition);
				// End control point.
				m_bezierSpline.SetControlPoint(m_splinePointCount, m_raycastedPosition);
				m_bezierSpline.SetControlPointMode(m_splinePointCount, BezierSpline.BezierControlPointMode.Free);
				// Tween control points.
				for (int i = 1; i < m_bezierSpline.getDimension(); ++i)
				{
					//m_bezierSpline.SetControlPointMode(m_splinePointCount - i, BezierSpline.BezierControlPointMode.Free);
					m_bezierSpline.SetControlPoint(m_splinePointCount - i, m_raycastedPosition);
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
					if (Vector3.Distance(m_raycastedPosition, segmentStartPointPosition) > m_newPointDistance)
					{
						m_currentState = DrawState.Adding;
					}
					else
					{
						for (int i = m_bezierSpline.getDimension(); i >= 1; --i)
						{
							//m_bezierSpline.SetControlPointMode(m_splinePointCount - i + 1, m_curveMode);
							Vector3 controlPointPosition = Vector3.Lerp(segmentStartPointPosition, m_raycastedPosition, (float)i / (float)m_bezierSpline.getDimension());
							m_bezierSpline.SetControlPoint(m_splinePointCount - m_bezierSpline.getDimension() + i, controlPointPosition);
						}
					}
				}
                
                // drawing is completed
				if (checkIsComplete())
				{
					// TODO: Remove this once we have a cleaner way to reset this.
					if (m_timeAngle)
					{
						m_timeAngle.reset();
					}

                    // get encompassed objects
                    GameObject[] controllables = GameObject.FindGameObjectsWithTag("TimeControllable");
                    for (int i = 0; i < controllables.Length; i++)
                    {
                        if (GeometryHelper.PolyContainsPoint(m_bezierSpline.GetPoints(), controllables[i].transform.position))
                        {
                            m_encompassed.Add(controllables[i]);
                            controllables[i].gameObject.GetComponent<TimeControllable>().Activate();
                        }
                    }

                    // activate global time controller
                    m_globalTime.GetComponent<AnimParameterController>().setAnimators(m_encompassed);
                    m_globalTime.GetComponent<AnimParameterController>().setActive(true);

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
            
            for(int i = 0; i < m_encompassed.Count; i++)
            {
                m_encompassed[i].GetComponent<TimeControllable>().Deactivate();
            }
            m_encompassed = new List<GameObject>();

			m_currentState = DrawState.Uncreated;
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}
