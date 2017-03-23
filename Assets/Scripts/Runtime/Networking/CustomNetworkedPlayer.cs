using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CustomNetworkedPlayer : NetworkBehaviour
{
    private GameObject m_spawnedCharacter;

	public override void OnStartClient()
	{
		base.OnStartClient();
		DontDestroyOnLoad(this);
	}

	public override void OnStartLocalPlayer() // this is our player
	{
		base.OnStartLocalPlayer();
	}

    public void resetPlayer(GameObject spawnedCharacter)
    {
        if (isLocalPlayer && spawnedCharacter)
        {
            tryRespawnPlayerObject(spawnedCharacter);
        }
    }
    public void tryRespawnPlayerObject(GameObject oldCharacter)
    {
        NetworkIdentity netId = this.GetComponent<NetworkIdentity>();
        if (netId)
        {
            NetworkConnection netConn = null;
            if (netId.clientAuthorityOwner != null)
            {
                netConn = netId.clientAuthorityOwner;
            }
            else
            {
                netConn = netId.connectionToServer;
            }
            if (netConn != null)
            {
                if (isClient)
                {
                    CmdRespawnCustomPlayer(netId, oldCharacter);
                }
            }
        }
    }

    [Command]
    public void CmdRespawnCustomPlayer(NetworkIdentity playerNetID, GameObject oldCharacter)
    {
        instantiateCustomPlayer(playerNetID, oldCharacter);
    }

    private void respawnCustomPlayer(NetworkIdentity playerNetID, GameObject oldCharacter)
    {
        NetworkConnection netConn = null;
        if (playerNetID.clientAuthorityOwner != null)
        {
            netConn = playerNetID.clientAuthorityOwner;
        }
        else
        {
            netConn = playerNetID.connectionToServer;
        }
        if (netConn != null)
        {
            Transform spawnTranform = null;
            if (oldCharacter)
            {
                spawnTranform = oldCharacter.transform;
                Debug.Log(spawnTranform.position);
            }
            if (spawnTranform == null)
            {
                spawnTranform = NetworkLobbyManager.singleton.startPositions[netConn.connectionId];
            }
            Debug.Assert(spawnTranform != null, "Could not find a spawn point for the custom networked character.");

            GameObject playerObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[netConn.connectionId], spawnTranform.position, spawnTranform.rotation, this.transform);
            for (int i = 0; i < playerObject.transform.GetChildCount(); i++)
            {
                playerObject.transform.GetChild(i).position = spawnTranform.position;
            }

            m_spawnedCharacter = playerObject;
            NetworkServer.SpawnWithClientAuthority(playerObject, netConn);
        }
    }

    public void tryInstantiatePlayerObject()
	{
		NetworkIdentity netId = this.GetComponent<NetworkIdentity>();
		if (netId)
		{
			NetworkConnection netConn = null;
			if (netId.clientAuthorityOwner != null)
			{
				netConn = netId.clientAuthorityOwner;
			}
			else
			{
				netConn = netId.connectionToServer;
			}
			if (netConn != null)
			{
				if (isClient)
				{
					CmdInstantiateCustomPlayer(netId, null);
				}
				//else if (isServer)
				//{
				//	RpcInstantiateCustomPlayer(netId, null);
				//}
			}
		}
	}

	public float m_spawnDelayTimer = 1;
	bool waitingForDelayedStart = true;
	void FixedUpdate()
	{
		if(isLocalPlayer)
		{
			if (waitingForDelayedStart)
			{
				m_spawnDelayTimer -= Time.deltaTime;
				if (m_spawnDelayTimer <= 0)
				{
					waitingForDelayedStart = false;
					tryInstantiatePlayerObject();
				}
			}
		}
	}

	[Command]
	public void CmdInstantiateCustomPlayer(NetworkIdentity playerNetID, GameObject spawnedObject)
	{
		instantiateCustomPlayer(playerNetID, spawnedObject);
	}

	[ClientRpc]
	public void RpcInstantiateCustomPlayer(NetworkIdentity playerNetID, GameObject spawnedObject)
	{
		instantiateCustomPlayer(playerNetID, spawnedObject);
	}

	private void instantiateCustomPlayer(NetworkIdentity playerNetID, GameObject spawnedObject)
	{
		NetworkConnection netConn = null;
		if (playerNetID.clientAuthorityOwner != null)
		{
			netConn = playerNetID.clientAuthorityOwner;
		}
		else
		{
			netConn = playerNetID.connectionToServer;
		}
		if(netConn != null)
		{
			Transform spawnTranform = null;
            GameObject spawnPointerManagerObject = GameObject.Find("SpawnPointManager");
			if (spawnPointerManagerObject)
			{
				SpawnPointManager spawnPointManager = spawnPointerManagerObject.GetComponent<SpawnPointManager>();
				if (spawnPointManager)
				{
					spawnTranform = spawnPointManager.getPlayerSpawnTransform(netConn.connectionId);
                    Debug.Log(spawnTranform.position);
				}
			}
			if(spawnTranform == null)
			{
				spawnTranform = NetworkLobbyManager.singleton.startPositions[netConn.connectionId];
			}
			Debug.Assert(spawnTranform != null, "Could not find a spawn point for the custom networked character.");

			GameObject playerObject = GameObject.Instantiate(NetworkLobbyManager.singleton.spawnPrefabs[netConn.connectionId], spawnTranform.position, spawnTranform.rotation, this.transform);
            for(int i = 0; i < playerObject.transform.GetChildCount(); i++)
            {
                playerObject.transform.GetChild(i).position = spawnTranform.position;
            }

            m_spawnedCharacter = playerObject;
            NetworkServer.SpawnWithClientAuthority(playerObject, netConn);
		}
	}

	[Command]
	public void CmdSetAuthority(NetworkIdentity playerNetID)
	{
		setAuthority(playerNetID);
	}

	private void setAuthority(NetworkIdentity playerNetID)
	{
		GameObject networkAuthorityGroup = GameObject.Find("NetworkAuthorityGroup");
		if (networkAuthorityGroup)
		{
			AssignPlayerAuthority assignPlayerAuthority = networkAuthorityGroup.GetComponent<AssignPlayerAuthority>();
			if (assignPlayerAuthority)
			{
				assignPlayerAuthority.assignAuthority(playerNetID.clientAuthorityOwner.connectionId, playerNetID.clientAuthorityOwner);
			}
		}
	}
}
