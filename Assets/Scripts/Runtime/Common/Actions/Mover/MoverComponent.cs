using UnityEngine;
using System;
using System.Collections.Generic;

using MOJ.Helpers;

/// <summary>
/// An component for controlling the movement of the entity.
/// </summary>
[Serializable]
public class MoverComponent : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////  

	[Flags]
	public enum ActionTypeMask
	{
		Run = 1 << 0,
		Jump = 1 << 1,
		Swim = 1 << 2,
		Slide = 1 << 3,
	}

	[Serializable]
	public class ActionMoverDictionary : SerializableDictionary<ActionTypeMask, Mover> { }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////   

	[SerializeField]
	private ActionTypeMask m_actionTypeMask = 0;
	[SerializeField]
	private ActionMoverDictionary m_actionMovers = new ActionMoverDictionary();
	private LinkedList<Mover> m_activeActionMovers = new LinkedList<Mover>();

	private Observer<MoverComponent> m_observer;

	private Transform m_transformComponent = null;

	[SerializeField]
	private bool m_alwaysUpdate = false;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////  

	public MoverComponent()
	{
		m_observer = new Observer<MoverComponent>(this);
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////  

	public ActionTypeMask getActionTypekMask() { return m_actionTypeMask; }
	public void setActionTypeMask(ActionTypeMask actionTypeMask) { m_actionTypeMask = actionTypeMask; }

	public bool getAlwaysUpdate() {	return m_alwaysUpdate; }
	public void setAlwaysUpdate(bool alwaysUpdate) { m_alwaysUpdate = alwaysUpdate; }

	public Observer<MoverComponent> getObserver() { return m_observer; }

	public Dictionary<ActionTypeMask, Mover> getActionMovers()
	{
		return m_actionMovers.Dictionary;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	public void createMoverAction(ActionTypeMask moverAction)
	{
		Mover mover;
		if (!m_actionMovers.Dictionary.TryGetValue(moverAction, out mover))
		{
			mover = new Mover();
			m_actionMovers.Dictionary.Add(moverAction, mover);
		}
	}

	public void removeMoverAction(ActionTypeMask moverAction)
	{
		m_actionMovers.Dictionary.Remove(moverAction);
	}

	public void activateMoverAction(ActionTypeMask actionTypeMask)
	{
		Mover mover;
		if (!m_actionMovers.Dictionary.TryGetValue(actionTypeMask, out mover))
		{
			createMoverAction(actionTypeMask);
			mover = m_actionMovers.Dictionary[actionTypeMask];
		}
		m_activeActionMovers.AddLast(mover);
		mover.beginMove(m_transformComponent);
	}

	protected void pauseMoverAction(ActionTypeMask actionTypeMask)
	{
		Mover mover;
		if (m_actionMovers.Dictionary.TryGetValue(actionTypeMask, out mover))
		{
			mover.pause();
		}
	}

	protected void resumeMoverAction(ActionTypeMask actionTypeMask)
	{
		Mover mover;
		if (m_actionMovers.Dictionary.TryGetValue(actionTypeMask, out mover))
		{
			mover.resume();
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	protected virtual void initialize()
	{
		m_transformComponent = GetComponent<Transform>();
		if (!m_alwaysUpdate)
		{
			this.enabled = false;
		}
	}

	protected virtual void updateMovers()
	{
		if (m_activeActionMovers.Count > 0)
		{
			LinkedListNode<Mover> moverNode = m_activeActionMovers.First;
			do
			{
				moverNode.Value.updateMove(m_transformComponent, Time.deltaTime);
				if (moverNode.Value.getState() == Mover.State.Finished)
				{
					m_activeActionMovers.Remove(moverNode);
				}
				moverNode = moverNode.Next;
			} while (moverNode != null);
		}
	}

	// Use this for initialization
	void Start()
	{
		initialize();
    }

	// Update is called once per frame
	void Update()
	{
		updateMovers();
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}