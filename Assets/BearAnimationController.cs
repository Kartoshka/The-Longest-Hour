using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearAnimationController : MonoBehaviour {


	public Transform trackedTransform;

	[Space(5)]
	[Header("Animation")]
	private DebugEntities debug;
	public Animator m_animator;
	public string m_idleParam;
	public string m_attackParam;
	public string m_runSpeedParam;
	public string m_aimParam;
	public string m_grenadeParam;

	public float m_moveDistanceScale;
	private Transform m_transform;
	private Vector3 m_prevPosition;
	// Update is called once per frame

	void Start()
	{
		if (trackedTransform == null)
		{
			m_transform = this.transform;
		} else
		{
			m_transform = trackedTransform;
		}

		m_prevPosition = m_transform.position;
	}

	void Update () {
		updateRun ();
	}


	public void updateRun(){
		Vector3 position = m_transform.position;
		float runSpeed = Vector3.Distance(position, m_prevPosition) * m_moveDistanceScale;
		m_prevPosition = position;

		if (m_animator.GetBool (m_idleParam))
		{	
			m_animator.SetFloat(m_runSpeedParam, runSpeed);
		}
	}


	public void doAttack(){
		if (!m_animator.GetBool (m_attackParam))
		{
			m_animator.SetBool (m_attackParam, true);
		}
		
	}

	public void startAim(){
		if (!m_animator.GetBool (m_aimParam))
		{
			m_animator.SetBool (m_aimParam, true);
		}
	}

	public void shootGrenade(){
		if (m_animator.GetBool (m_aimParam))
		{
			m_animator.SetTrigger (m_grenadeParam);
		}
	}

	public void endAim()
	{
		if (m_animator.GetBool (m_aimParam))
		{
			m_animator.SetBool (m_aimParam, false);
		}
	}

	public bool getControllerTrigger(string state, out CharacterControllerStateTrigger result){
		
		CharacterControllerStateTrigger[] behaviours = m_animator.GetBehaviours<CharacterControllerStateTrigger> ();
		foreach (CharacterControllerStateTrigger b in behaviours)
		{
			if (b.s_id == state)
			{
				result = b;
				return true;
			}
		}
		result = null;
		return false;
	}
}
