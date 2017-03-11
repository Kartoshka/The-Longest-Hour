using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerSelect : NetworkBehaviour
{
	public Toggle m_playerSelectedToggle;
	public bool isServerPlayer = true;

	[SyncVar]
	private bool m_isReady = false;

	public bool getIsReady() { return m_isReady; }
	public void setIsReady(bool isReady) { m_isReady = isReady; }

	//public bool canToggle(Toggle toggle)
	//{
	//	if(toggle.)
	//}

	//[Command] // Ask the server to make the change.
	//private void CmdToggleReady(bool isReady)
	//{
	//	RpcToggleReady(isReady);
	//}

	//[ClientRpc]  // Make the change.
	//private void RpcToggleReady(bool isReady)
	//{
	//	setIsReady(isReady);
	//}

	//[Command]
	//void CmdServerAssignClient()
	//{
	//	this.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
	//}

	//// Reference: http://stackoverflow.com/questions/33469930/how-do-i-sync-non-player-gameobject-properties-in-unet-unity5
	//[Command]
	//void CmdServerRemoveClient()
	//{
	//	GameObject CarServer = GameObject.Find("AssaultVehicle");

	//	CarServer.GetComponent<NetworkIdentity>().RemoveClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);

	//}

	//[Command] // Ask the server to make the change.
	//void CmdServerAssignClient(GameObject obj, Color col)
	//{
	//	objNetId = obj.GetComponent<NetworkIdentity>();        // get the object's network ID
	//	objNetId.AssignClientAuthority(connectionToClient);    // assign authority to the player who is changing the color
	//	RpcPaint(obj, col);                                    // usse a Client RPC function to "paint" the object on all clients
	//	objNetId.RemoveClientAuthority(connectionToClient);    // remove the authority from the player who changed the color
	//}

	//[ClientRpc] // Make the change.
	//void RpcPaint(GameObject obj, Color col)
	//{
	//	obj.GetComponent<Renderer>().material.color = col;      // this is the line that actually makes the change in color happen
	//}

	// Reference: http://stackoverflow.com/questions/33469930/how-do-i-sync-non-player-gameobject-properties-in-unet-unity5
	//[Command]
	//void CmdAssignObjectAuthority(NetworkInstanceId netInstanceId)
	//{
	//	// Assign authority of this objects network instance id to the client
	//	NetworkServer.objects[netInstanceId].AssignClientAuthority(connectionToClient);
	//}

	//[Command]
	//void CmdRemoveObjectAuthority(NetworkInstanceId netInstanceId)
	//{
	//	// Removes the  authority of this object network instance id to the client
	//	NetworkServer.objects[netInstanceId].RemoveClientAuthority(connectionToClient);
	//}


	public void ToggleSelect()
	{
		bool canToggle = (isServerPlayer && this.isServer) || (!isServerPlayer && this.isClient && !this.isServer);
        if (canToggle && this.isLocalPlayer)
		{
			m_isReady = !m_isReady;
			if (m_isReady)
			{
				m_playerSelectedToggle.interactable = true;
			}
			//CmdToggleReady(m_isReady);
			setIsReady(m_isReady);
		}
		else
		{
			m_playerSelectedToggle.interactable = false;
			m_playerSelectedToggle.isOn = false;
        }
	}
}
