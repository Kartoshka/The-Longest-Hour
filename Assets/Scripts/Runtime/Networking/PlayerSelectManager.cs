using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Prototype.NetworkLobby;

public class PlayerSelectManager : NetworkBehaviour {

	public List<PlayerSelect> m_playerSelections;
	public float prematchCountdown = 5.0f;
	public string nextScene = "";
	public LobbyManager m_lobbyManager;

	// Use this for initialization
	void Start () {
		this.enabled = true;

		// Reset the ready states.
		foreach (PlayerSelect playerSelect in m_playerSelections)
		{
			playerSelect.setIsReady(false);
		}
		GameObject lobbyObject = GameObject.FindGameObjectWithTag("LobbyManager");
		if(lobbyObject)
		{
			m_lobbyManager = lobbyObject.GetComponent<LobbyManager>();
			if(m_lobbyManager)
			{
				for(int i = 0; i < m_lobbyManager.lobbySlots.Length; ++i)
				{
					NetworkIdentity netId = m_playerSelections[i].GetComponent<NetworkIdentity>();
					if(netId)
					{
						netId.AssignClientAuthority(m_lobbyManager.lobbySlots[i].connectionToClient);
                    }
                }
            }
		}
    }

	public NetworkConnection getPlayerNetId(int playerIndex)
	{
		NetworkConnection netConnect = null;
		if(m_lobbyManager && playerIndex < m_lobbyManager.lobbySlots.Length)
		{
			netConnect = m_lobbyManager.lobbySlots[playerIndex].connectionToClient;
		}
		
        return netConnect;
    }

	private void loadScene()
	{
		Debug.Assert(nextScene != "", "No scene declared.");
		SceneManager.LoadScene(nextScene);
    }
	
	// Update is called once per frame
	void Update () {
		int isReady = 0;
		foreach(PlayerSelect playerSelect in m_playerSelections)
		{
			if(playerSelect.getIsReady())
			{
				++isReady;
			}
		}
		if(isReady == m_playerSelections.Count)
		{
			StartCoroutine(ServerCountdownCoroutine());
			this.enabled = false;
        }
	}

	public IEnumerator ServerCountdownCoroutine()
	{
		float remainingTime = prematchCountdown;
		int floorTime = Mathf.FloorToInt(remainingTime);

		while (remainingTime > 0)
		{
			yield return null;

			remainingTime -= Time.deltaTime;
		}

		if(m_lobbyManager)
		{
			m_lobbyManager.ServerChangeScene(nextScene);
		}
		else
		{
			SceneManager.LoadScene(nextScene);
		}
    }
}
