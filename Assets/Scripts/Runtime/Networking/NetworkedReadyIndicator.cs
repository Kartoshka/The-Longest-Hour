using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkIdentity))]
public class NetworkedReadyIndicator : NetworkBehaviour
{
	public ReadyIndicator m_readyIndicator;

	public bool getIsReady() { return m_readyIndicator.getIsReady(); }
	public void setIsReady(bool isReady)
	{
		if (hasAuthority)
		{
			CmdSetIsReady(isReady);
		}
	}
	public void toggleIsReady()
	{
		if (hasAuthority)
		{
			CmdToggleIsReady();
		}
	}

	[Command] // Ask the server to make the change.
	private void CmdSetIsReady(bool isReady)
	{
		RpcSetIsReady(isReady);
	}

	[ClientRpc] // Make the change.
	private void RpcSetIsReady(bool isReady)
	{
		m_readyIndicator.setIsReady(isReady);
	}


	[Command] // Ask the server to make the change.
	private void CmdToggleIsReady()
	{
		RpcToggleIsReady();
	}
	[ClientRpc] // Make the change.
	private void RpcToggleIsReady()
	{
		m_readyIndicator.setIsReady(!m_readyIndicator.getIsReady());
	}
}
