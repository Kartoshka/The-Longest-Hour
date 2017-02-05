using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEntities : MonoBehaviour
{
	public List<GameObject> m_activateOnStart;
	public List<GameObject> m_deactivateOnStart;


	// Use this for initialization
	void Start () {
		foreach(GameObject gameObject in m_activateOnStart)
		{
			gameObject.SetActive(true);
		}
		foreach (GameObject gameObject in m_deactivateOnStart)
		{
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
