using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssignPlayerAuthority : NetworkBehaviour
{
	public List<NetworkIdentity> m_toBeAssignedToP1;
	public List<NetworkIdentity> m_toBeAssignedToP2;

	void Start()
	{
		if (Network.isServer)
		{
			RpcSetAuthority();
		}
		else if (isServer)
		{
			RpcSetAuthority();
		}
		else if (isClient)
		{
			// Do nothing;
		}
	}

	void OnLevelWasLoaded()
	{
		if(Network.isServer)
		{
			RpcSetAuthority();
        }
		else if (isServer)
		{
			RpcSetAuthority();
		}
		else if(isClient)
		{
			// Do nothing;
		}
	}

	[ClientRpc] // Make the change.
	private void RpcSetAuthority(NetworkConnection netConn)
	{
		assignAuthority(netConn.connectionId, netConn);
    }

	public void assignAuthority(int playerId, NetworkConnection netConn)
	{
		List<NetworkIdentity> assignments = null;
		switch(playerId)
		{
			case (0):
			{
				assignments = m_toBeAssignedToP1;
            }
			break;
			case (1):
			{
				assignments = m_toBeAssignedToP2;
			}
			break;
		}
        if (assignments != null)
		{
			foreach(NetworkIdentity netId in assignments)
			{
				netId.AssignClientAuthority(netConn);
			}
		}
	}
}
