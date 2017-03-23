using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControllable : MonoBehaviour
{
    public Material m_taggedMaterial;
    public Material m_activeMaterial;
    public Material m_inactiveMaterial;
    public GameObject airTag;
	public PhysicsTimeCtrl m_physicsController;

    public bool isCar;
    bool timeControllable;
    bool tagged;
    bool circled;

    Renderer renderer;

	// Use this for initialization
	void Start ()
    {
		m_physicsController = gameObject.GetComponent<PhysicsTimeCtrl> ();
        renderer = gameObject.GetComponent<Renderer>();
        airTag.SetActive(false);
        tagged = false;
        circled = false;
        timeControllable = false;
        isCar = false;
	}

    private void Update()
    {
        if(!timeControllable && tagged && circled)
        {
            Activate();
            timeControllable = true;
        }
        else if(timeControllable && (!tagged || !circled))
        {
            Deactivate();
            timeControllable = false;
        }
    }

    public void Tag()
    {
        if (isCar)
        {
            transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[1] = m_taggedMaterial;
            return;
        }

        foreach (MeshRenderer childRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            childRenderer.material = m_taggedMaterial;
        }
        foreach (GroundTagFlashing childFlashing in gameObject.GetComponentsInChildren<GroundTagFlashing>())
        {
            childFlashing.enabled = true;
        }

        tagged = true;
    }

    public void Untag()
    {
        if (isCar)
        {
            transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[1] = m_inactiveMaterial;
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

        tagged = false;
    }

    public void Circle()
    {
        airTag.SetActive(true);
        circled = true;
    }

    public void Uncircle()
    {
        airTag.SetActive(false);
        circled = false;
    }

    public void Activate()
    {
        if(isCar)
        {
            transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[1] = m_activeMaterial;
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
    }

    public void Deactivate()
    {
        if (isCar)
        {
            transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[1] = m_inactiveMaterial;
            airTag.SetActive(false);
            return;
        }


        foreach (MeshRenderer childRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (tagged)
            {
                childRenderer.material = m_taggedMaterial;
            }
            else
            {
                childRenderer.material = m_inactiveMaterial;
            }
        }
        foreach (GroundTagFlashing childFlashing in gameObject.GetComponentsInChildren<GroundTagFlashing>())
        {
            childFlashing.enabled = false;
        }

		if (m_physicsController)
		{
			m_physicsController.isModifiable = false;
		}
    }
}
