using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationManager : MonoBehaviour
{

	public List<GameObject> m_activeOnStartObjects = new List<GameObject>();
	public List<MonoBehaviour> m_activeOnStartComponents = new List<MonoBehaviour>();

	public List<GameObject> m_deactiveOnStartObjects = new List<GameObject>();
	public List<MonoBehaviour> m_deactiveOnStartComponents = new List<MonoBehaviour>();

	// Use this for initialization
	void Start()
	{
		foreach (GameObject gameObject in m_activeOnStartObjects)
		{
			gameObject.SetActive(true);
		}
		foreach (MonoBehaviour behaviour in m_activeOnStartComponents)
		{
			behaviour.enabled = true;
		}
		foreach (GameObject gameObject in m_deactiveOnStartObjects)
		{
			gameObject.SetActive(true);
		}
		foreach (MonoBehaviour behaviour in m_deactiveOnStartComponents)
		{
			behaviour.enabled = true;
		}
	}
}
