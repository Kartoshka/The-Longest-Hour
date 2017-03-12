using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolParamsOnExit : StateMachineBehaviour {


	public List<string> m_onParams;
	public List<string> m_offParams;

	//OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		foreach(string param in m_onParams)
		{
			animator.SetBool(param, true);
		}
		foreach (string param in m_offParams)
		{
			animator.SetBool(param, false);
		}
	}
}
