using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimEvent : MonoBehaviour
{
	public List<GameObject> m_enableObjectsOnAttackStart;
	public List<GameObject> m_disableObjectsOnAttackEnd;

	public void onAttackBegin()
	{
		foreach(GameObject gameObject in m_enableObjectsOnAttackStart)
		{
			gameObject.SetActive(true);
		}
	}

	public void onAttackEnd()
	{
		foreach (GameObject gameObject in m_enableObjectsOnAttackStart)
		{
			gameObject.SetActive(false);
		}
	}
}
