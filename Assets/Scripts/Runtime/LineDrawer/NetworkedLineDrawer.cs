using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedLineDrawer : NetworkBehaviour
{
	public LineDrawerComponent m_lineDrawerComponent;

	// TODO: Remove hardcoded keys.
	// inputKey: 1 = add; 2 = reset; Should match LineDrawerComponent
	public void processInputs()
	{
		int inputKey = -1;
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
		{
			inputKey = 1;
        }
		if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
		{
			inputKey = 2;
		}

		if(inputKey == 1 || inputKey == 2)
		{
			CmdProcessInputs(inputKey);
		}
    }

	//private void ProcessInputs()
	//{
	//	if (m_currentState == DrawState.Uncreated)
	//	{
	//		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
	//		{
	//			m_currentState = DrawState.Adding;
	//		}
	//	}
	//	if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
	//	{
	//		reset();

	//		for (int i = 0; i < m_encompassed.Count; i++)
	//		{
	//			m_encompassed[i].GetComponent<TimeControllable>().Uncircle();
	//		}
	//		m_encompassed = new List<GameObject>();

	//		m_currentState = DrawState.Uncreated;
	//	}
	//}

	[Command]
	public void CmdProcessInputs(int inputKey)
	{
		RpcProcessInputs(inputKey);
	}

	[ClientRpc]
	public void RpcProcessInputs(int inputKey)
	{
		m_lineDrawerComponent.updateState(inputKey);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		processInputs();
    }

	//private void ProcessInputs()
	//{
	//	if (m_currentState == DrawState.Uncreated)
	//	{
	//		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
	//		{
	//			m_currentState = DrawState.Adding;
	//		}
	//	}
	//	if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
	//	{
	//		reset();

	//		for (int i = 0; i < m_encompassed.Count; i++)
	//		{
	//			m_encompassed[i].GetComponent<TimeControllable>().Uncircle();
	//		}
	//		m_encompassed = new List<GameObject>();

	//		m_currentState = DrawState.Uncreated;
	//	}
	//}
}
