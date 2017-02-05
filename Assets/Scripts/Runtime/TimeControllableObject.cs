using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllable : MonoBehaviour {

    public Material m_activeMaterial;
    public Material m_inactiveMaterial;

    Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate()
    {
        renderer.material = m_activeMaterial;
    }

    public void Deactivate()
    {
        renderer.material = m_inactiveMaterial;
    }
}
