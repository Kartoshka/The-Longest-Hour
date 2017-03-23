using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedBearInputController : NetworkBehaviour
{
	public BearInputController m_inputController;

	private float m_prev_rTrgr;
	private float m_prev_lTrgr;
	private bool m_prev_isFiring;
	private bool m_prev_isAiming;

	// Use this for initialization
	void Start()
	{
		if (!base.isServer)
		{
			m_inputController.enabled = false;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (isServer)
		{
			processInputs();
		}
	}

	// TODO: Remove hardcoded keys.
	// inputKey: 1 = attack; 21 = startAim; 22 = stopAim; Should match BearInputController
	public void processInputs()
	{
		float rTrgr = Mathf.Clamp01(Input.GetAxis("RightTrigger"));
		float lTrgr = Mathf.Clamp01(Input.GetAxis("LeftTrigger"));
		if(rTrgr != m_prev_rTrgr || lTrgr != m_prev_lTrgr)
		{
			m_prev_rTrgr = rTrgr;
			m_prev_lTrgr = lTrgr;
			CmdSetTime(rTrgr - lTrgr);
		}

		bool isFiring = Input.GetButtonDown("Fire1");
		bool isAiming = Input.GetButton("AimBear");
		if (isFiring != m_prev_isFiring || isAiming != m_prev_isAiming)
		{
			m_prev_isFiring = isFiring;
			m_prev_isAiming = isAiming;
			CmdProcessInputs(isFiring, isAiming);
		}
	}

	[Command]
	public void CmdSetTime(float time)
	{
		RpcSetTime(time);
	}

	[ClientRpc]
	public void RpcSetTime(float time)
	{
		m_inputController.setTime(time);
    }

	[Command]
	public void CmdProcessInputs(bool isFiring, bool isAiming)
	{
		RpcProcessInputs(isFiring, isAiming);
	}

	[ClientRpc]
	public void RpcProcessInputs(bool isFiring, bool isAiming)
	{
		m_inputController.processInputs(isFiring, isAiming);
	}
}
