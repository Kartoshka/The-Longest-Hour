using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllable : MonoBehaviour
{

    public Material m_activeMaterial;
    public Material m_inactiveMaterial;
    public GameObject airTag;
	public PhysicsTimeCtrl m_physicsController;

    public bool isCar;
    bool timeControllable;

    Renderer renderer;

	// Use this for initialization
	void Start ()
    {
		m_physicsController = gameObject.GetComponent<PhysicsTimeCtrl> ();
        renderer = gameObject.GetComponent<Renderer>();
        timeControllable = false;
        isCar = false;
	}
	

    public void Activate()
    {
        if(isCar)
        {
            transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[1] = m_activeMaterial;
            timeControllable = true;
            airTag.SetActive(true);
            return;
        }
        
        foreach(MeshRenderer childRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            childRenderer.material = m_activeMaterial;
        }
        foreach (GroundTagFlashing childFlashing in gameObject.GetComponentsInChildren<GroundTagFlashing>())
        {
            childFlashing.enabled = true;
        }
		if (m_physicsController)
		{
			m_physicsController.isModifiable = true;
		}
        timeControllable = true;
        airTag.SetActive(true);
    }

    public void Deactivate()
    {
        if (isCar)
        {
            transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[1] = m_inactiveMaterial;
            airTag.SetActive(false);
            timeControllable = false;
            return;
        }


        foreach (MeshRenderer childRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            childRenderer.material = m_inactiveMaterial;
        }
        foreach (GroundTagFlashing childFlashing in gameObject.GetComponentsInChildren<GroundTagFlashing>())
        {
            childFlashing.enabled = false;
        }

		if (m_physicsController)
		{
			m_physicsController.isModifiable = false;
		}
        airTag.SetActive(false);
        timeControllable = false;
    }
}
