using UnityEngine;
using System.Collections;

using MOJ.Helpers;

/// <summary>
///
/// </summary>
public class TimeShiftController : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////


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

	public AnimParameterController m_animCtrl;
	public PhysicsParameterController m_physicsCtrl;
	public float m_rate = 1.0f;

	public bool m_canShiftTime = true;
    private float m_timeIncSize = 0.05f;
	private float m_time = 0.0f;

    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////
    #region Accessors
    //////////////////////////////////////////////////////////////////////////////////////////

    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////
    #region Methods
    //////////////////////////////////////////////////////////////////////////////////////////  

    public void setCanShiftTime(bool canShift)
    {
        m_canShiftTime = canShift;
    }
    
	private bool canShiftTime()
	{
		return m_canShiftTime;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{
		findParamControllers();
    }

	public void findParamControllers()
	{
		GameObject globalTimeControllerObject = GameObject.Find("GlobalTimeController");
		if(globalTimeControllerObject)
		{
			m_animCtrl = globalTimeControllerObject.GetComponent<AnimParameterController>();
			m_physicsCtrl = globalTimeControllerObject.GetComponent<PhysicsParameterController>();
		}
		//m_animCtrl = FindObjectOfType<AnimParameterController>();
		//m_physicsCtrl = FindObjectOfType<PhysicsParameterController>();
	}

	//bool m_isDelayedUpdate = true;
	//float m_delayUpdateCounter = 15.0f;

	// Update is called once per frame
	void Update()
	{
		//if(m_isDelayedUpdate)
		//{
		//	m_delayUpdateCounter -= Time.deltaTime;
		//	if(m_delayUpdateCounter <= 0.0f)
		//	{
		//		m_animCtrl = FindObjectOfType<AnimParameterController>();
		//		m_physicsCtrl = FindObjectOfType<PhysicsParameterController>();
		//		m_isDelayedUpdate = false;
		//	}
		//}

		ProcessInputs();

		//m_time = Mathf.Clamp01(m_time);
		if (m_animCtrl)
		{
			m_animCtrl.incParamValue(m_animCtrl.timeParameter, m_time);
		}
	}

	private void ProcessInputs()
	{
		if(canShiftTime())
		{
//            if (Input.GetKey(KeyCode.E))
//            {
//                m_time = m_timeIncSize;
//            }
//
//            if (Input.GetKey(KeyCode.Q))
//            {
//                m_time = -1 * m_timeIncSize;
//            }
            //m_time += m_rate * (Input.GetKey(KeyCode.E) ? 1 : 0) * Time.deltaTime;
			//m_time -= m_rate * (Input.GetKey(KeyCode.Q) ? 1 : 0) * Time.deltaTime;
		}
	}

	public void setTime(float t){
		t = Mathf.Clamp (t,-1.0f,1.0f);
		if(m_physicsCtrl)
		{
			m_physicsCtrl.setTime(t);
		}
		m_time = t;

	}
	//public void rewind(){
	//	m_time = -1 * m_timeIncSize;
	//}

	//public void forward(){
	//	m_time = m_timeIncSize;
	//}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}