using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkedGrenadeControllerComponent : NetworkBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public GrenadeControllerComponent m_grenadeControllerComponent;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	private Observer<GrenadeControllerComponent>.Listener m_grenadeControllerListener;   // For listening to when the non-networked component fired a projectile.

	private GameObject m_projectile;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Listen to the CollisionObserver to detect pickups.
	private Observer<GrenadeControllerComponent>.Listener createGrenadeListener()
	{
		m_grenadeControllerListener = delegate (GrenadeControllerComponent grenadeController)
		{
			m_projectile = grenadeController.getSpawnedProjectile();
			CmdFireProjectile();
        };
		return m_grenadeControllerListener;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Network
	//////////////////////////////////////////////////////////////////////////////////////////  

	[Command]
	private void CmdFireProjectile()
	{
		NetworkServer.Spawn(m_projectile);
		//setRollValue(value);
		//RpcRollDiceAction(value);
	}

	//[ClientRpc]
	//private void RpcRollDiceAction(int value)
	//{
	//	setRollValue(value);
	//}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{
		//if (!Network.peerType.Equals(NetworkPeerType.Disconnected))
		//{
			if (m_grenadeControllerComponent)
			{
				m_grenadeControllerComponent.enabled = false;
				m_grenadeControllerListener = createGrenadeListener();
				m_grenadeControllerComponent.getObserver().add(m_grenadeControllerListener);
			}
		//}
		//else
		//{
		//	this.enabled = false;
		//}
	}

	// Update is called once per frame
	void Update()
	{
		if (isLocalPlayer)
		{
			m_grenadeControllerComponent.update();
		}
	}

	#endregion
}
