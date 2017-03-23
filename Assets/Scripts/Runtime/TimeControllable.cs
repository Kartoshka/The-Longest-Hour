﻿using System.Collections;
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
    public Animator myAnimator;
    Renderer renderer;

	// Use this for initialization
	void Start ()
    {
		m_physicsController = gameObject.GetComponent<PhysicsTimeCtrl> ();
        renderer = gameObject.GetComponent<Renderer>();

        Animator temp = GetComponent<Animator>();
        if(temp)
        {
            myAnimator = temp;
        }
        else
        {
            myAnimator = GetComponentInParent<Animator>();
        }

        airTag.SetActive(false);
        tagged = false;
        circled = false;
        timeControllable = false;
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
            SkinnedMeshRenderer smr = transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            Material[] temp = smr.materials;
            temp[1] = m_taggedMaterial;
            smr.materials = temp;
            tagged = true;
            airTag.SetActive(true);
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
            SkinnedMeshRenderer smr = transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            Material[] temp = smr.materials;
            temp[1] = m_inactiveMaterial;
            smr.materials = temp;
            tagged = false;
            airTag.SetActive(false);
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
            SkinnedMeshRenderer smr = transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            Material[] temp = smr.materials;
            temp[1] = m_activeMaterial;
            smr.materials = temp;
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
            SkinnedMeshRenderer smr = transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            Material[] temp = smr.materials;
            temp[1] = m_inactiveMaterial;
            smr.materials = temp;
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
