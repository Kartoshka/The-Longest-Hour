using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagSingle : MonoBehaviour
{
	public CollisionObserver m_collisionObserver;

	private bool m_isInTaggableTrigger;
    private GameObject m_tagTarget;
	private Observer<CollisionObserver>.Listener m_collisionListener;

	// Use this for initialization
	void Start ()
    {
		m_isInTaggableTrigger = false;
		if (m_collisionObserver)
		{
			m_collisionListener = createCollisionListener();
			m_collisionObserver.getObserver().add(m_collisionListener);
		}
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetButtonDown("Fire1"))
        {
            if(m_isInTaggableTrigger)
            {
				toggleTagging();
            }
        }
	}

	private Observer<CollisionObserver>.Listener createCollisionListener()
	{
		m_collisionListener = delegate (CollisionObserver collisionObserver)
		{
			Collider collider = collisionObserver.getPreviousCollider();
			if (collider != null)
			{
				if (collider.gameObject.tag == "TimeControllable")
				{
					bool isEntering = collisionObserver.getIsEntering();
                    m_isInTaggableTrigger = isEntering;
					m_tagTarget = isEntering ? collider.gameObject : null;
				}
			}
		};
		return m_collisionListener;
	}

	void toggleTagging()
    {
        if(m_tagTarget.GetComponent<TimeControllable>().enabled)
        {
			m_tagTarget.GetComponent<TimeControllable>().Deactivate();
			m_tagTarget.GetComponent<TimeControllable>().enabled = false;
        }
		else
		{
			m_tagTarget.GetComponent<TimeControllable>().enabled = true;
			m_tagTarget.GetComponent<TimeControllable>().Activate();
		}
    }
}
