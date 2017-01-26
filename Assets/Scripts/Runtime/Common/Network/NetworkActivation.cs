using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetworkActivation : NetworkBehaviour {

	public List<GameObject> m_localOnlyObjects = new List<GameObject>();

	// Use this for initialization
	void Start ()
	{
		if(!Network.peerType.Equals(NetworkPeerType.Disconnected))
		{
			if (this.isLocalPlayer)
			{
				foreach (GameObject gameObject in m_localOnlyObjects)
				{
					gameObject.SetActive(true);
				}
			}
			else
			{
				foreach (GameObject gameObject in m_localOnlyObjects)
				{
					gameObject.SetActive(false);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
