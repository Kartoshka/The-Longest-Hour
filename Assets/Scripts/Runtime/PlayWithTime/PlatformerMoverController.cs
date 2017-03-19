using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

using MOJ.Helpers;

/// <summary>
/// If this is a component:
///     Retrieves Unity information and passes it to the abstract data type.
/// </summary>
public class PlatformerMoverController : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////


	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public MoverComponent m_moverComponent;
	public float m_turnRate = 2.0f;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	private Mover m_runMover = null;
	private Mover m_jumpMover = null;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	// Use this for initialization
	void Start()
	{
		if (m_moverComponent)
		{
			m_moverComponent.getActionMovers().TryGetValue(MoverComponent.ActionTypeFlag.Run, out m_runMover);
			m_moverComponent.getActionMovers().TryGetValue(MoverComponent.ActionTypeFlag.Jump, out m_jumpMover);
		}
	}

	// Update is called once per frame
	void Update()
	{
		ProcessInputs();
	}

	private void ProcessInputs()
	{
        
        float horizontalInput;
        if (Network.peerType == NetworkPeerType.Disconnected)
            horizontalInput = Input.GetAxis("HorizontalGround");
        else
            horizontalInput = Input.GetAxis("Horizontal");

        float depthInput = Input.GetAxis("Vertical");
        if (Network.peerType == NetworkPeerType.Disconnected)
            depthInput = Input.GetAxis("VerticalGround");
        else
            depthInput = Input.GetAxis("Vertical");

        if (horizontalInput != 0 || depthInput != 0)
		{
			m_moverComponent.activateMoverAction(MoverComponent.ActionTypeFlag.Run);

			if (m_runMover != null)
			{
				if (!m_runMover.getMoverBehavior().getMoveRelative())
				{
					m_moverComponent.getTransform().forward = new Vector3(horizontalInput, 0, depthInput);
				}
			}
		}

		float yawInput = Input.GetAxis("HorizontalRightStick");
		//float pitchInput = Input.GetAxis("VerticalRightStick");

		if (yawInput != 0 )//|| pitchInput != 0)
		{
			//m_moverComponent.getTransform().Rotate(new Vector3(0, m_turnRate * yawInput, 0));
		}

		if ((Network.peerType == NetworkPeerType.Disconnected && Input.GetButtonDown("JumpGround")) ||
           Network.peerType != NetworkPeerType.Disconnected && Input.GetButtonDown("Jump"))
		{
			m_moverComponent.activateMoverAction(MoverComponent.ActionTypeFlag.Jump);
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}