using UnityEngine;
using System.Collections;

using MOJ.Helpers;

/// <summary>
/// If this is a component:
///     Retrieves Unity information and passes it to the abstract data type.
/// </summary>
public class ERMoverController : MonoBehaviour
{
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Datatypes
	//////////////////////////////////////////////////////////////////////////////////////////


	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region GameObjects
	//////////////////////////////////////////////////////////////////////////////////////////

	public MoverComponent m_moverComponent;
	public SlopedSurfaceBoolProvider m_slideBoolProvider;

	public RaycastData m_raycastData;

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Constructors
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Attributes
	//////////////////////////////////////////////////////////////////////////////////////////

	private Mover m_runMover = null;
	private Mover m_slideMover = null;
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
		if(m_moverComponent)
		{
			m_moverComponent.getActionMovers().TryGetValue(MoverComponent.ActionTypeFlag.Run, out m_runMover);
			m_moverComponent.getActionMovers().TryGetValue(MoverComponent.ActionTypeFlag.Jump, out m_jumpMover);
			m_moverComponent.getActionMovers().TryGetValue(MoverComponent.ActionTypeFlag.Slide, out m_slideMover);
        }
    }

	// Update is called once per frame
	void Update()
	{
		ProcessInputs();

		if (m_slideMover != null)
		{
			bool isSloped = false;
			//RaycastData raycastData;
			//raycastData.checkDistance = 10.0f;
			//raycastData.direction = this.gameObject.transform.up * -1;
			//raycastData.sourceTransform = this.gameObject.transform;
			//raycastData.surfaceLayerMask = 1 << 8 ;
			Vector3 surfaceGradient;
            if(GeometryHelper.tryFindSurfaceGradient(ref m_raycastData, out surfaceGradient))
			{
				float surfaceThreshold = 0.01f;
				if(surfaceGradient.magnitude > surfaceThreshold)
				{
					isSloped = true;
                }
			}
			if(isSloped)
			{
				m_slideMover.resume();
			}
			else
			{
				m_slideMover.pause();
			}
		}
	}

	private void ProcessInputs()
	{
		if(Input.GetAxis("Horizontal") != 0)
		{
			m_moverComponent.activateMoverAction(MoverComponent.ActionTypeFlag.Run);
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if(m_slideMover != null)
			{
				m_slideMover.pause();
            }
			m_moverComponent.activateMoverAction(MoverComponent.ActionTypeFlag.Jump);
		}
    }

	#endregion
	//////////////////////////////////////////////////////////////////////////////////////////
	#region Persistence
	//////////////////////////////////////////////////////////////////////////////////////////

	#endregion
}