using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkMovingEntity : NetworkBehaviour
{
	public MovingEntity m_movingEntity; 

	// Use this for initialization
	void Start ()
	{
		if(m_movingEntity)
		{
			m_movingEntity.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isServer)
		{
			if (m_movingEntity)
			{
				m_movingEntity.update();
			}
		}
	}
}
