using UnityEngine;
using System;
using MOJ.Helpers;

[Serializable]
public class Mover
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////

	public enum BehaviorType
	{
		// Ensure createMoverBehavior(..) also has the Behavior Type.
		Undefined = -1,
		LinearInput,
		RigidBodyForceInput,
	}

	public enum State
	{
		Undefined,
		Active,
		Paused,
		Finished
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////


	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	[SerializeField]
	private BehaviorType m_behaviorType = BehaviorType.Undefined;
	[SerializeField]
	private MoverBehavior m_behavior = null;
	[SerializeField]
	private MoverInterpolationData m_interpData = null;

	private State m_state = State.Undefined;
	private float m_elapsedTime = 0.0f;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	public Mover()
	{
		m_interpData = new MoverInterpolationData();
	}

	public void deInit()
	{
		m_behaviorType = BehaviorType.Undefined;
		m_behavior = null;
		m_interpData = null;
		m_state = State.Undefined;
    }
	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public BehaviorType getBehaviorType() { return m_behaviorType;	}
	public void setBehaviorType(BehaviorType behaviorType)
	{
		if(m_behaviorType != behaviorType)
		{
			m_behaviorType = behaviorType;
			m_behavior = createMoverBehavior(behaviorType, m_behavior);
		}
	}

	public MoverBehavior getBehavior() { return m_behavior; }

	public State getState() { return m_state; }
	private void setState(State state) { m_state = state; }

	public InterpolationHelper.InterpolationType getInterpolationType() { return m_interpData.interpolationType; }
	public void setInterpolationType(InterpolationHelper.InterpolationType interpolationType) { m_interpData.interpolationType = interpolationType; }

	public InterpolationHelper.EasingType getEasingType() { return m_interpData.easingType; }
	public void setEasingType(InterpolationHelper.EasingType easingType) { m_interpData.easingType = easingType; }

	public float getUpdateRate() { return m_interpData.updateRate; }
	public void setUpdateRate(float updateRate) { m_interpData.updateRate = updateRate; }

	public float getDuration() { return m_interpData.duration; }
	public void setDuration(float duration) { m_interpData.duration = duration; }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	private InterpolationHelper.Vector3Interpolator createInterpolatorFromData(Transform transform)
	{
		Vector3 deltaPosition = m_behavior.getTargetPosition(transform) - transform.position;
		return new InterpolationHelper.Vector3Interpolator(
													m_interpData.interpolationType,
													m_interpData.easingType,
													transform.position,
													deltaPosition,
													m_interpData.duration);
	}

	private MoverBehavior createMoverBehavior(BehaviorType moverBehaviorType, MoverBehavior copiedBehavior = null)
	{
		// Update this whenever a new Mover Behavior is created.
		MoverBehavior newMoverBehavior = null;
		switch (moverBehaviorType)
		{
			case (BehaviorType.LinearInput):
			{
				newMoverBehavior = ScriptableObject.CreateInstance<LinearInputMoverBehavior>();
			}
			break;
			case (BehaviorType.RigidBodyForceInput):
			{
				newMoverBehavior = ScriptableObject.CreateInstance<RigidBodyForceMoverBehavior>();
			}
			break;
		}

		if (newMoverBehavior != null)
		{
			if (copiedBehavior != null)
			{
				newMoverBehavior.initialize(copiedBehavior);
			}
			else
			{
				newMoverBehavior.initialize();
			}
		}

		return newMoverBehavior;
	}

	public void pause()
	{
		if(getState() == State.Active)
		{
			setState(State.Paused);
		}
	}
	public void resume()
	{
		if (getState() == State.Paused)
		{
			setState(State.Active);
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	public void beginMove(Transform transform)
	{
		m_state = State.Active;

		if (m_behavior.getCanInterpolate())
		{
			m_behavior.setInterpolator(createInterpolatorFromData(transform));
			m_behavior.resetPosition();
		}
		updateMove(transform, 0.0f);
	}

	public void updateMove(Transform transform, float deltaTime)
	{
		if (m_state == State.Active)
		{
			m_elapsedTime += Time.fixedDeltaTime;
			if (m_elapsedTime > m_interpData.updateRate)
			{
				if (!m_behavior.tryMove(transform, deltaTime))
				{
					endMove();
				}
				m_elapsedTime = 0;
			}
		}
	}

	private void endMove()
	{
		m_state = State.Finished;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}