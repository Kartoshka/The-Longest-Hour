﻿using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// An action command for changing the position of the owner of a Mover component.
/// </summary>
public class MoveAction : ActionCommand
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	protected MoverComponent m_moverComponent;
	//private MoverComponent.MoverListener m_moverListener;
	private Observer<MoverComponent>.Listener m_moverListener;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	public MoveAction(MoverComponent moverComponent)
	{
		Debug.Assert(moverComponent != null, "Error: MoverComponent provided was null.");
		m_moverComponent = moverComponent;
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
				if (moverComponent.getState() == MoverComponent.MoverState.Finished)
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
		m_moverComponent.beginMove();
		return true;
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}
