using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedBirdController : NetworkBehaviour
{
	public BirdController m_birdController;

	private int m_prevInput = -1;

	// Use this for initialization
	void Start () {
		if(base.isServer)
		{
			m_birdController.enabled = false;
		}
    }
	
	// Update is called once per frame
	void Update () {
		if (!isServer)
		{
			processInputs();
		}
	}

	// TODO: Remove hardcoded keys.
	// inputKey: 1 = add; 2 = reset; Should match BirdController
	public void processInputs()
	{
		int inputKey = -1;

		float rightTrigger = Input.GetAxis("RightTrigger");
		if (rightTrigger == 1)
		{
			inputKey = 0;
        }
		// if detected divable object, press x to dive to it
		else if (Input.GetButtonDown("Fire3"))
		{
			inputKey = 1;
        }
		// otherwise, reset abilities
		else
		{
			inputKey = 2;
        }

		if (m_prevInput != inputKey && inputKey >= 0 && inputKey <= 2)
		{
			m_prevInput = inputKey;
			CmdProcessInputs(inputKey);
		}
	}

	[Command]
	public void CmdProcessInputs(int inputKey)
	{
		RpcProcessInputs(inputKey);
	}

	[ClientRpc]
	public void RpcProcessInputs(int inputKey)
	{
		m_birdController.processInputs(inputKey);
	}
}
