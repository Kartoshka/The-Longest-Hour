using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// An action command for changing the position of the owner of a Mover component.
/// </summary>
public class MoveAction : ActionCommand
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	private MoverComponent.ActionTypeFlag m_actionTypeFlag;
	protected MoverComponent m_moverComponent;
	//private MoverComponent.MoverListener m_moverListener;
	private Observer<MoverComponent>.Listener m_moverListener;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	public MoveAction(MoverComponent moverComponent, MoverComponent.ActionTypeFlag actionTypeFlag)
	{
		Debug.Assert(moverComponent != null, "Error: MoverComponent provided was null.");
		m_moverComponent = moverComponent;
		m_actionTypeFlag = actionTypeFlag;
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	private Observer<MoverComponent>.Listener createMoverListener()
	{
		m_moverListener = delegate (MoverComponent moverComponent)
			{
				if (moverComponent.getActionState(m_actionTypeFlag) == Mover.State.Finished)
				{
					//moverComponent.removeListener(m_moverListener);
					moverComponent.getObserver().remove(m_moverListener);
					onFinished();
				}
			};
		return m_moverListener;
    }

	// Returns true if the action was successfully executed.
	public override bool execute()
	{
		m_moverComponent.getObserver().add(createMoverListener());
		m_moverComponent.activateMoverAction(m_actionTypeFlag);
		return true;
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}
