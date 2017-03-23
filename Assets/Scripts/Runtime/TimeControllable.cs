using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TimeControllable : NetworkBehaviour
{
    public Material m_taggedMaterial;
    public Material m_circledMaterial;
    public Material m_activeMaterial;
    public Material m_inactiveMaterial;
    public GameObject airTag;
	public PhysicsTimeCtrl m_physicsController;

    public bool isCar;
    bool timeControllable;

    [SyncVar]
    bool tagged;

    [SyncVar]
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
        if (!timeControllable)
        {
            if (isCar)
            {
                SkinnedMeshRenderer smr = transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
                Material[] temp = smr.materials;
                temp[1] = m_taggedMaterial;
                smr.materials = temp;
                CmdTag(true);
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

            CmdTag(true);
        }
    }

    [Command]
    void CmdTag(bool val)
    {
        tagged = val;
    }

    public void Untag()
    {
        if (isCar)
        {
            SkinnedMeshRenderer smr = transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            Material[] temp = smr.materials;
            if (circled)
            {
                temp[1] = m_circledMaterial;
            }
            else
            {
                temp[1] = m_inactiveMaterial;
            }
            
            smr.materials = temp;
            CmdTag(false);
            return;
        }

        foreach (MeshRenderer childRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (circled)
            {
                childRenderer.material = m_circledMaterial;
            } else
            {
                childRenderer.material = m_inactiveMaterial;
            }
            
        }
        if (!circled)
        {
            foreach (GroundTagFlashing childFlashing in gameObject.GetComponentsInChildren<GroundTagFlashing>())
            {
                childFlashing.enabled = false;
            }
        }
        
        CmdTag(false);
    }

    public void Circle()
    {
        if (isCar)
        {
            SkinnedMeshRenderer smr = transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            Material[] temp = smr.materials;
            temp[1] = m_circledMaterial;
            smr.materials = temp;
            CmdCircled(true);
            airTag.SetActive(true);
            return;
        }

        foreach (MeshRenderer childRenderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            childRenderer.material = m_circledMaterial;
        }
        foreach (GroundTagFlashing childFlashing in gameObject.GetComponentsInChildren<GroundTagFlashing>())
        {
            childFlashing.enabled = true;
        }

        airTag.SetActive(true);
        CmdCircled(true);
    }

    [Command]
    void CmdCircled(bool val)
    {
        circled = val;
    }

    public void Uncircle()
    {
        if (isCar)
        {
            SkinnedMeshRenderer smr = transform.parent.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            Material[] temp = smr.materials;
            if (tagged)
            {
                temp[1] = m_taggedMaterial;
            }
            else
            {
                temp[1] = m_inactiveMaterial;
            }

            smr.materials = temp;
            CmdCircled(false);
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
        if (!tagged)
        {
            foreach (GroundTagFlashing childFlashing in gameObject.GetComponentsInChildren<GroundTagFlashing>())
            {
                childFlashing.enabled = false;
            }
        }

        airTag.SetActive(false);
        CmdCircled(false);
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
            if (tagged)
            {
                temp[1] = m_taggedMaterial;
            }
            else if (circled)
            {
                temp[1] = m_circledMaterial;
            }
            else
            {
                temp[1] = m_inactiveMaterial;
            }
            
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
            else if (circled)
            {
                childRenderer.material = m_circledMaterial;
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
