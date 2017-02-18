using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;

public class DrawTargetting : SelectableAbility {

	//The mover behaviour that takes care of actually making the bird dive

	private bool drawing = false;

	private bool initialized = false;

    public LineDrawerComponent m_lineDrawer;

	protected override void OnEnableTargeting()
	{
        m_lineDrawer = gameObject.GetComponent<LineDrawerComponent>();
	}

	protected override void OnActivate ()
	{
        if (!drawing)
        {
			m_lineDrawer.ClearDrawing ();
			drawing = true;
            m_lineDrawer.StartDrawing();
        } 
	}

	protected override void OnDisactivate ()
	{
		if (m_lineDrawer.currentState != LineDrawerComponent.DrawState.Complete) {
			m_lineDrawer.ClearDrawing ();
		}
		drawing = false;
	}

	protected override void OnDisableTargeting()
	{
        //captureModule.Release ();
        
    }

	public void disableAnimCamera(){
		
	}
}
