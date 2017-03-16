using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkActivation : NetworkBehaviour {

	public List<GameObject> m_offliineOnlyObjects = new List<GameObject>();
	public List<MonoBehaviour> m_offlineOnlyBehaviours = new List<MonoBehaviour>();

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
		foreach (GameObject gameObject in m_offliineOnlyObjects)
		{
			gameObject.SetActive(isActive);
		}
		foreach (MonoBehaviour behaviour in m_offlineOnlyBehaviours)
		{
			behaviour.enabled = isActive;
		}
	}
}
