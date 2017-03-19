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
			Observer<CollisionObserver> observer = m_collisionObserver.getObserver();
			observer.add(m_collisionListener);
			m_collisionObserver.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
//		if(Input.GetButtonDown("Fire1"))
//        {
//            if(m_isInTaggableTrigger)
//            {
//				toggleTagging();
//            }
//        }
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
					activateTimeControl();
                }
			}
		};
		return m_collisionListener;
	}

	private void activateTimeControl()
	{
		TimeControllable timeControllable = m_tagTarget.GetComponent<TimeControllable>();
        if (timeControllable)
		{
			m_tagTarget.GetComponent<TimeControllable>().enabled = true;
			m_tagTarget.GetComponent<TimeControllable>().Activate();
		}
	}

	private void deactivateTimeControl()
	{
		TimeControllable timeControllable = m_tagTarget.GetComponent<TimeControllable>();
		if (timeControllable)
		{
			m_tagTarget.GetComponent<TimeControllable>().Deactivate();
			m_tagTarget.GetComponent<TimeControllable>().enabled = false;
		}
	}


	public void toggleTagging()
    {
		if (!m_tagTarget || !m_tagTarget.GetComponent<TimeControllable> ())
		{
			return;
		}
		if(m_tagTarget.GetComponent<TimeControllable>().enabled)
        {
			deactivateTimeControl();
        }
		else
		{
			activateTimeControl();
        }
    }

	//// TODO: Debug Hack. Perhaps remove.
	//void OnTriggerEnter(Collider collider)
	//{
	//	if (collider != null)
	//	{
	//		if (collider.gameObject.tag == "TimeControllable")
	//		{
	//			m_isInTaggableTrigger = true;
	//			m_tagTarget = true ? collider.gameObject : null;
	//		}
	//	}
	//}
}
