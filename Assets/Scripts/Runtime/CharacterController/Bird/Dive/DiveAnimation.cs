using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiveAnimation : MoverBehavior {

	public delegate void CallBack ();

	public CallBack onDiveComplete;

	public AnimationCurve heightAnimation;

	private Vector3 initialPosition;
	private float targetHeight;
	private float heightDrop;

	public float duration;
	private float elapsedTime = 0;
	private bool m_active = false;
	public bool active{
		get{ return m_active;}
	}

	public void initialize(Transform initialPosition, float targetHeight)
	{
		initialize ();
		heightDrop = initialPosition.position.y - targetHeight;
		this.initialPosition = initialPosition.position;
		this.targetHeight = targetHeight;
		m_active = true;
	}

	public override void initialize ()
	{
		base.initialize ();
	}

	public override void initialize (MoverBehavior copiedBehavior)
	{
		base.initialize (copiedBehavior);
	}

	public override void processInputs ()
	{
		base.processInputs ();
	}

	public override Vector3 getTargetPosition (Transform transform, float deltaTime)
	{
		if (elapsedTime+Time.deltaTime > duration) {
			m_active = false;
			elapsedTime = 0;
			onDiveComplete ();
		}

		if (m_active) {
			elapsedTime += Time.deltaTime;

			float height = heightAnimation.Evaluate (elapsedTime / duration);

			return new Vector3 (initialPosition.x, height*(initialPosition.y-targetHeight) + targetHeight , initialPosition.z);

		} else {
			return transform.position;
		}


	}
}
