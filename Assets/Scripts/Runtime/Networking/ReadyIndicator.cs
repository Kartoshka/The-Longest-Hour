using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkIdentity))]
public class ReadyIndicator : MonoBehaviour
{
	public Color m_readyColour = Color.green;
	public Color m_notReadyColour = Color.red;
	public Image m_indicatorImage;

	private bool m_isReady = false;

	public void setIsReady(bool isReady)
	{
		m_isReady = isReady;
		if (m_indicatorImage)
		{
			m_indicatorImage.color = m_isReady ? m_readyColour : m_notReadyColour;
		}
	}
	public bool getIsReady() { return m_isReady; }
}