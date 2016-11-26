using UnityEngine;
using System;
using System.Collections.Generic;

using MOJ.Helpers;

/// <summary>
/// An component for controlling the movement of the entity.
/// </summary>
public class MoverComponent : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////  
	
	public enum MoverState
	{
		Idle,
		Paused,
		Active,
		Finished
	} 

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////   

	[SerializeField]
	private MoverComponentData m_data = new MoverComponentData();
	private MoverState m_currentState = MoverState.Idle;
	[SerializeField]
	private MoverBehavior m_moverBehavior = null;

	private InterpolationHelper.Vector3Interpolator m_interpolator = null;
	private Observer<MoverComponent> m_observer;

	private Transform m_transformComponent = null;
	private float m_elapsedTime = 0.0f;
	[SerializeField]
	private bool m_alwaysUpdate = false;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////  

	public MoverComponent()
	{
		m_observer = new Observer<MoverComponent>(this);
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////  

	public MoverComponentData.MoverType getMoverType() { return m_data.moverType; }
	public void setMoverType(MoverComponentData.MoverType moverType)
	{
		m_data.moverType = moverType;

		switch (moverType)
		{
			case (MoverComponentData.MoverType.Linear):
			{
				//m_moverBehavior = new LinearInputMoverBehavior(m_moverBehavior);
				m_moverBehavior = ScriptableObject.CreateInstance<LinearInputMoverBehavior>();
			}
			break;
			default:
			{
				m_moverBehavior = null;
			}
			break;
		}

		if(m_moverBehavior != null)
		{
			m_moverBehavior.initialize(m_moverBehavior);
		}
	}

	public MoverState getState() { return m_currentState; }
    private void setState(MoverState state)
	{
		m_currentState = state;
		m_observer.notify();
	}

	public InterpolationHelper.InterpolationType getInterpolationType() { return m_data.interpolationType; }
	public void setInterpolationType(InterpolationHelper.InterpolationType interpolationType) { m_data.interpolationType = interpolationType; }

	public InterpolationHelper.EasingType getEasingType() { return m_data.easingType; }
	public void setEasingType(InterpolationHelper.EasingType easingType) { m_data.easingType = easingType; }

	public float getUpdateRate() { return m_data.updateRate; }
	public void setUpdateRate(float updateRate) { m_data.updateRate = Mathf.Max(0, updateRate); }

	public float getDuration() { return m_data.duration; }
	public void setDuration(float duration) { m_data.duration = Mathf.Max(0.01f, duration); }

	public bool getAlwaysUpdate() { return m_alwaysUpdate; }
	public void setAlwaysUpdate(bool alwaysUpdate) { m_alwaysUpdate = alwaysUpdate; }

	public MoverBehavior getMoverBehavior() { return m_moverBehavior; }

	public Observer<MoverComponent> getObserver() { return m_observer; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	private InterpolationHelper.Vector3Interpolator createInterpolatorFromData()
	{
		Vector3 deltaPosition = m_moverBehavior.getTargetPosition(m_transformComponent) - m_transformComponent.position;
		return new InterpolationHelper.Vector3Interpolator(
													m_data.interpolationType,
													m_data.easingType,
													m_transformComponent.position,
													deltaPosition,
													m_data.duration);
	}

	public void beginMove()
	{
		this.enabled = true; // Start updating.
		setState(MoverState.Active);

		if (m_moverBehavior.getCanInterpolate())
		{
			m_interpolator = createInterpolatorFromData();
			m_moverBehavior.setInterpolator(m_interpolator);
			m_moverBehavior.resetPosition();
		}
		updateMove(0.0f);
    }

	private void updateMove(float deltaTime)
	{
		if (!m_moverBehavior.tryMove(m_transformComponent, deltaTime))
		{
			endMove();
		}
	}

	private void endMove()
	{
		if (!m_alwaysUpdate)
		{
			this.enabled = false;
		}
		setState(MoverState.Finished);
		setState(MoverState.Idle);
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{
		m_transformComponent = GetComponent<Transform>();
		if(!m_alwaysUpdate)
		{
			this.enabled = false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		m_elapsedTime += Time.fixedDeltaTime;
		if (m_elapsedTime > m_data.updateRate)
		{
			updateMove(m_elapsedTime);
			m_elapsedTime = 0;
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}