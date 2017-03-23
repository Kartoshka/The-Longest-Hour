using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkActivation : NetworkBehaviour {

	public List<GameObject> m_offlineOnlyObjects = new List<GameObject>();
	public List<MonoBehaviour> m_offlineOnlyBehaviours = new List<MonoBehaviour>();

	public List<GameObject> m_serverOnlyObjects = new List<GameObject>();
	public List<MonoBehaviour> m_serverOnlyBehaviours = new List<MonoBehaviour>();
	public List<GameObject> m_clientOnlyObjects = new List<GameObject>();
	public List<MonoBehaviour> m_clientOnlyBehaviours = new List<MonoBehaviour>();

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

		setServerClientActivation(base.isServer);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void setServerClientActivation(bool isServer)
	{
		foreach (GameObject gameObject in m_serverOnlyObjects)
		{
			gameObject.SetActive(isServer);
		}
		foreach (MonoBehaviour behaviour in m_serverOnlyBehaviours)
		{
			behaviour.enabled = isServer;
		}

		foreach (GameObject gameObject in m_clientOnlyObjects)
		{
			gameObject.SetActive(!isServer);
		}
		foreach (MonoBehaviour behaviour in m_clientOnlyBehaviours)
		{
			behaviour.enabled = !isServer;
		}
	}

	private void setActive(bool isActive)
	{
		foreach (GameObject gameObject in m_offlineOnlyObjects)
		{
			gameObject.SetActive(isActive);
		}
		foreach (MonoBehaviour behaviour in m_offlineOnlyBehaviours)
		{
			behaviour.enabled = isActive;
		}
	}
}
