using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkActivation : NetworkBehaviour {

	public List<GameObject> m_localOnlyObjects = new List<GameObject>();
	public List<MonoBehaviour> m_localOnlyBehaviours = new List<MonoBehaviour>();

	// Use this for initialization
	void Start ()
	{
		if(!Network.peerType.Equals(NetworkPeerType.Disconnected))
		{
			if (this.isLocalPlayer)
			{
				setActive(true);
			}
			else
			{
				setActive(false);
            }
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void setActive(bool isActive)
	{
		foreach (GameObject gameObject in m_localOnlyObjects)
		{
			gameObject.SetActive(isActive);
		}
		foreach (MonoBehaviour behaviour in m_localOnlyBehaviours)
		{
			behaviour.enabled = isActive;
		}
	}
}
