using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllable : MonoBehaviour
{

    public Material m_activeMaterial;
    public Material m_inactiveMaterial;
    public GameObject airTag;

    bool timeControllable;

    Renderer renderer;

	// Use this for initialization
	void Start ()
    {
        renderer = gameObject.GetComponent<Renderer>();
        timeControllable = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Activate()
    {
        foreach(MeshRenderer childRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            childRenderer.material = m_activeMaterial;
        }
        foreach (GroundTagFlashing childFlashing in gameObject.GetComponentsInChildren<GroundTagFlashing>())
        {
            childFlashing.enabled = true;
        }
        timeControllable = true;
        airTag.SetActive(true);
    }

    public void Deactivate()
    {
        foreach (MeshRenderer childRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            childRenderer.material = m_inactiveMaterial;
        }
        foreach (GroundTagFlashing childFlashing in gameObject.GetComponentsInChildren<GroundTagFlashing>())
        {
            childFlashing.enabled = false;
        }
        airTag.SetActive(false);
        timeControllable = false;
    }
}
