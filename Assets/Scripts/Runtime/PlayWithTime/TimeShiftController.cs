﻿using UnityEngine;
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

	}

	// Update is called once per frame
	void Update()
	{
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
            if (Input.GetKey(KeyCode.E))
            {
                m_time = m_timeIncSize;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                m_time = -1 * m_timeIncSize;
            }
            //m_time += m_rate * (Input.GetKey(KeyCode.E) ? 1 : 0) * Time.deltaTime;
			//m_time -= m_rate * (Input.GetKey(KeyCode.Q) ? 1 : 0) * Time.deltaTime;
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}