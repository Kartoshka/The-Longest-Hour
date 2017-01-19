using UnityEngine;
using System;
using System.Collections.Generic;

using MOJ.Helpers;

/// <summary>
/// An component for controlling the movement of the entity.
/// </summary>
[Serializable]
[AddComponentMenu("MOJCustom/Mover/Mover Component")]
public class MoverComponent : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////  

	[Flags]
	public enum ActionTypeFlag
	{
		Run = 1 << 0,
		Jump = 1 << 1,
		Swim = 1 << 2,
		Slide = 1 << 3,
	}

	[Serializable]
	public class ActionMoverDictionary : SerializableDictionary<ActionTypeFlag, Mover> { }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////   

	[SerializeField]
	private ActionTypeFlag m_actionTypeFlags = 0;
	[SerializeField]
	private ActionMoverDictionary m_actionMovers = new ActionMoverDictionary();
	private LinkedList<Mover> m_activeActionMovers = new LinkedList<Mover>();

	private Observer<MoverComponent> m_observer;

	[SerializeField]
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

	protected virtual void initialize()
	{
		if (m_transformComponent == null)
		{
			m_transformComponent = GetComponent<Transform>();
		}
		if (!m_alwaysUpdate)
		{
			this.enabled = false;
		}

		int actionTypeFlagCount = System.Enum.GetValues(typeof(ActionTypeFlag)).Length;
		for (int i = 0; i < actionTypeFlagCount; ++i)
		{
			ActionTypeFlag mask = (ActionTypeFlag)(1 << i);
			ActionTypeFlag maskedActionTypeFlag = mask & m_actionTypeFlags;
			if (maskedActionTypeFlag != 0)
			{
				activateMoverAction(maskedActionTypeFlag);
			}
		}
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Accessors
	//////////////////////////////////////////////////////////////////////////////////////////  

	public ActionTypeFlag getActionTypeFlags() { return m_actionTypeFlags; }
	public void setActionTypeFlags(ActionTypeFlag actionTypeFlags) { m_actionTypeFlags = actionTypeFlags; }

	public Transform getTransform() { return m_transformComponent; }
	public void setTransform(Transform transform) { m_transformComponent = transform; }

	public bool getAlwaysUpdate() {	return m_alwaysUpdate; }
	public void setAlwaysUpdate(bool alwaysUpdate) { m_alwaysUpdate = alwaysUpdate; }

	public Observer<MoverComponent> getObserver() { return m_observer; }

	public Dictionary<ActionTypeFlag, Mover> getActionMovers()
	{
		return m_actionMovers.Dictionary;
	}

	public Mover.State getActionState(ActionTypeFlag actionTypeFlag)
	{
		Mover.State state = Mover.State.Undefined;
		Mover mover;
		if (m_actionMovers.Dictionary.TryGetValue(actionTypeFlag, out mover))
		{
			state = mover.getState();
		}
		return state;
	}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Methods
	//////////////////////////////////////////////////////////////////////////////////////////  

	public void createMoverAction(ActionTypeFlag moverAction)
	{
		Mover mover;
		if (!m_actionMovers.Dictionary.TryGetValue(moverAction, out mover) || mover == null)
		{
			mover = new Mover();
			m_actionMovers.Dictionary.Add(moverAction, mover);
		}
	}

	public void removeMoverAction(ActionTypeFlag moverAction)
	{
		Mover mover;
		if (m_actionMovers.Dictionary.TryGetValue(moverAction, out mover) && mover != null)
		{
			mover.deInit();
		}
		m_actionMovers.Dictionary.Remove(moverAction);
	}

	public void activateMoverAction(ActionTypeFlag actionTypeFlag)
	{
		Mover mover;
		if (!m_actionMovers.Dictionary.TryGetValue(actionTypeFlag, out mover))
		{
			Debug.Assert(false, "Could not find a mover for " + actionTypeFlag.ToString() + ". Default mover is created but may not behave as expected.");
			createMoverAction(actionTypeFlag);
			mover = m_actionMovers.Dictionary[actionTypeFlag];
		}
		if(!mover.getState().Equals(Mover.State.Active))
		{
			m_activeActionMovers.AddLast(mover);
			mover.beginMove(m_transformComponent);
		}
	}

	//public void pauseMoverAction(ActionTypeFlag actionTypeFlag)
	//{
	//	Mover mover;
	//	if (m_actionMovers.Dictionary.TryGetValue(actionTypeFlag, out mover))
	//	{
	//		mover.pause();
	//	}
	//}

	//public void resumeMoverAction(ActionTypeFlag actionTypeFlag)
	//{
	//	Mover mover;
	//	if (m_actionMovers.Dictionary.TryGetValue(actionTypeFlag, out mover))
	//	{
	//		mover.resume();
	//	}
	//}

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Runtime
	//////////////////////////////////////////////////////////////////////////////////////////  

	public void update()
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
		update();
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}