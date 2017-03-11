using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InitializeNetworkedObjects : NetworkBehaviour
{
	public List<NetworkIdentity> m_networkedObjects;

	[ClientRpc]
	private void RpcSpawnObjects()
	{
		foreach (NetworkIdentity networkId in m_networkedObjects)
		{
			GameObject gameObject = Instantiate<GameObject>(networkId.gameObject, networkId.gameObject.transform.position, networkId.gameObject.transform.rotation);
			NetworkServer.Spawn(gameObject);
			Destroy(networkId.gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
		if(this.isServer)
		{
			RpcSpawnObjects();
		}
    }
}
