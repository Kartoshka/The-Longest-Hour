using UnityEngine;

public class LineWalker : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////

	public enum SplineWalkerMode
	{
		Once,
		Loop,
		PingPong
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	public BezierSpline m_spline;
	public SplineWalkerMode m_mode;
	public float m_duration;
	public bool m_lookForward;

	private bool m_isActive = false;
	private float m_progress;
	private float m_startPoint = 0.0f;
	private float m_endPoint = 1.0f;
	private bool m_goingForward = true;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public BezierSpline getSpline() { return m_spline; }
	public void setSpline(BezierSpline spline) { m_spline = spline; }

	public void setStartPoint(float startPoint) { m_startPoint = startPoint; }
	public void setEndPoint(float endPoint) { m_endPoint = endPoint; }
	public bool getIsActive() { return m_isActive; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	public void play()
	{
		m_isActive = true;
    }

	public void pause()
	{
		m_isActive = false;
	}

	public void stop()
	{
		m_isActive = false;
		m_progress = m_startPoint;
	}

	public void reset()
	{
		m_progress = m_startPoint;
	}

	private void walk()
	{
		if(m_spline)
		{
			if (m_goingForward)
			{
				m_progress += Time.deltaTime / m_duration;
				if (m_progress > m_endPoint)
				{
					switch (m_mode)
					{
						case (SplineWalkerMode.Once):
						{
							m_progress = m_endPoint;
							break;
						}
						case (SplineWalkerMode.Loop):
						{
							m_progress = m_startPoint;
							break;
						}
						case (SplineWalkerMode.PingPong):
						{
							m_progress = m_endPoint;
							m_goingForward = false;
							break;
						}
					}
				}
			}
			else
			{
				m_progress -= Time.deltaTime / m_duration;
				if (m_progress < m_startPoint)
				{
					m_progress = m_startPoint;
					m_goingForward = true;
				}
			}

			Vector3 position = m_spline.GetPoint(m_progress);
            transform.localPosition = new Vector3(position.x, -8f, position.z) ;
			if (m_lookForward)
			{
				transform.LookAt(position + m_spline.GetDirection(m_progress));
			}
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (m_isActive)
		{
			walk();
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}
