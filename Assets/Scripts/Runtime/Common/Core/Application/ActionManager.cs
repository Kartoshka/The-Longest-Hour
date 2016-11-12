using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("")] // Hidden within inspector.
public class ActionManager
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	////////////////////////////////////////////////////////////////////////////////////////// 

	public enum State
	{
		Inactive,
		RunningAction,
		FinishedAction
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	protected Queue<ActionCommand> m_actions = new Queue<ActionCommand>();
	private ActionCommand m_currentAction = null;
	private State m_currentState = State.Inactive;

	private Observer<ActionManager> m_observer;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	public ActionManager()
	{
		m_observer = new Observer<ActionManager>(this);
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	public State getState() { return m_currentState; }
	private void setState(State state)
	{
		m_currentState = state;
		m_observer.notify();
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	private bool hasActions()
	{
		return m_actions.Count > 0;
	}

	public void queueAction(ActionCommand action)
	{
		if (action != null)
		{
			m_actions.Enqueue(action);
			if(m_currentState == State.Inactive)
			{
				executeNextAction();
			}
		}
	}

	private void executeNextAction()
	{
		if (hasActions())
		{
			m_currentAction = m_actions.Dequeue();
			executeAction(m_currentAction);
		}
		else
		{
			setState(State.Inactive);
		}
	}

	public virtual bool executeAction(ActionCommand action)
	{
		bool success = false;
		if (action != null)
		{
			m_currentAction = action;
			setState(State.RunningAction);
			success = action.execute();
		}
		return success;
	}

	public void clearQueuedActions()
	{
		m_actions.Clear();
	}

	public Observer<ActionManager> getObserver()
	{
		return m_observer;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////

	//public void initialize()
	//{

	//}

	public void deinitialize()
	{
		clearQueuedActions();
		m_currentAction = null;
	}

	public void onActionFinished()
	{
		m_currentAction = null;
		setState(State.FinishedAction);
		executeNextAction();
	}

	#endregion
}
