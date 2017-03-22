using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwap : MonoBehaviour
{
	public Material m_altMaterial;
	public SkinnedMeshRenderer m_skinnedMeshRenderer;

	private Material m_defaultMaterial;

	void Start()
	{
		if(m_skinnedMeshRenderer)
		{
			m_defaultMaterial = m_skinnedMeshRenderer.material;
		}
	}

	public void setDefaultMaterial()
	{
		if (m_skinnedMeshRenderer)
		{
			m_skinnedMeshRenderer.material = m_defaultMaterial;
		}
	}

	public void setAltMaterial()
	{
		if (m_skinnedMeshRenderer)
		{
			m_skinnedMeshRenderer.material = m_altMaterial;
		}
	}
}
