using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
	public List<GameObject> m_playerInputObjects;
	public GameObject m_visualEntity;

	private bool m_isDamaged = false;

	public float m_blinkTimer = 2.0f;
	private float m_currentBlinkTime = 0.0f;
	private bool m_isVisible = false;
	public float m_frozenTimer = 5.0f;
	private float m_currentFrozenTime = 0.0f;

	public void applyDamage()
	{
		if(!m_isDamaged)
		{
			m_isDamaged = true;
			//setPlayerInputsActive(false);
		}
    }

	private void updateDamageEffects()
	{
		m_currentFrozenTime += Time.deltaTime;
		if (m_currentFrozenTime > m_frozenTimer)
		{
			m_currentFrozenTime = 0.0f;
			recovered();
		}

		if (m_visualEntity)
		{
			m_currentBlinkTime += Time.deltaTime;
			if(m_currentBlinkTime > m_blinkTimer)
			{
				m_currentBlinkTime = 0.0f;
				m_isVisible = !m_isVisible;
            }

			if(!m_isDamaged)
			{
				m_isVisible = true;
			}
			m_visualEntity.SetActive(m_isVisible);
		}
	}

	private void recovered()
	{
		m_isDamaged = false;
		//setPlayerInputsActive(true);
    }

	private void setPlayerInputsActive(bool isActive)
	{
		foreach (GameObject gameObject in m_playerInputObjects)
		{
			//gameObject.SetActive(isActive);
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(m_isDamaged)
		{
			updateDamageEffects();
        }
	}
}
