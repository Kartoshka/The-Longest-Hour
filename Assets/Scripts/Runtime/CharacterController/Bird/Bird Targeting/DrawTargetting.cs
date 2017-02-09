using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine.Utility;

public class DrawTargetting : AbAirTargeting {

	//The mover behaviour that takes care of actually making the bird dive

	private bool drawing = false;

	private bool initialized = false;

    public LineDrawerComponent m_lineDrawer;

	protected override void OnEnableTargeting()
	{
        m_lineDrawer = gameObject.GetComponent<LineDrawerComponent>();

	}

	protected override void OnTriggerTargeting ()
	{
        if (!drawing)
        {
            m_lineDrawer.StartDrawing();
        } else
        {
            m_lineDrawer.ClearDrawing();
        }
        drawing = !drawing;
	}

	protected override void OnDisableTargeting()
	{
        //captureModule.Release ();
        
    }

	public void disableAnimCamera(){
		
	}
}
