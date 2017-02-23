using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class AnimParameterController : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public List<Animator> m_animators;
	public string timeParameter;

    private bool m_active = false;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region  Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Interface Methods
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////  

	public float m_startTime = 0;

    #endregion
    //////////////////////////////////////////////////////////////////////////////////////////
    #region Methods
    //////////////////////////////////////////////////////////////////////////////////////////

    public void setParamValue(string parameterName, float value)
    {
        if (m_active) { 
            foreach (Animator animator in m_animators)
            {
                animator.SetFloat(parameterName, value);
                animator.SetTime(value * 250);
            }
        }
	}

    // increment time
    public void incParamValue(string parameterName, float value)
    {
        if (m_active)
        {
            foreach (Animator animator in m_animators)
            {
                float totalTime = animator.GetCurrentAnimatorStateInfo(0).length;
                float incrementedTime = (float) (animator.GetTime() + value);

                if (incrementedTime > totalTime)
                    incrementedTime = totalTime;
                if (incrementedTime < 0)
                    incrementedTime = 0;

                float newTime = Mathf.Clamp01(incrementedTime / totalTime);
                animator.SetFloat(parameterName, newTime);
                animator.SetTime(incrementedTime);
            }
        }
    }

    public void setAnimators(List<GameObject> objs)
    {
        m_animators = new List<Animator>();
        foreach (GameObject go in objs)
        {
            Animator anim = go.GetComponent<Animator>();
            float currTime = (float) anim.GetTime();
            float totalTime = anim.GetCurrentAnimatorStateInfo(0).length;
            anim.SetFloat("time", currTime / totalTime);

            m_animators.Add(anim);
        }
    }

    public void setActive(bool active)
    {
        m_active = active;
    }

    public bool getActive()
    {
        return m_active;
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////

	void Start()
	{
		//setParamValue(timeParameter, m_startTime);
    }


	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}