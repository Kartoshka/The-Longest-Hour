using UnityEngine;
using System.Collections;
using Cinemachine.Utility;
using Cinemachine.Attributes;
using Cinemachine.Blending;
using Cinemachine.Assets;
using Cinemachine;


public class Cinemachine3rdPerson :MonoBehaviour  {


	public bool modifiable = true;
	public CinemachineVirtualCamera controlledView;

	private bool toggleView = false;
	//
	// Target Tracking Information
	//
	private Vector3 targetCurrentPosition;

	public float distanceFromTarget = 0;
	public Vector2 angleFromTarget = Vector2.zero;


	//Current rotation In degrees (TODO make into properties and use accessor methods to clamp them?)
	private float pitch = 0;
	private float yaw = 0;

	private Vector3 cameraPosition = Vector3.zero;

	public float minXAngle = -90;
	public float maxXAngle = 90;


	public void UpdatePosition()
	{
		if (modifiable) {
			targetCurrentPosition = controlledView.CameraTransposerTarget.transform.position;

			float appliedYaw = yaw + angleFromTarget.y;
			float appliedPitch = pitch + angleFromTarget.x;

			pitch = Mathf.Clamp (pitch, minXAngle, maxXAngle);

			//Polar to cartesian conversion
			float X = Mathf.Sin (Mathf.Deg2Rad * appliedPitch) * Mathf.Cos (Mathf.Deg2Rad * appliedYaw);
			float Z = Mathf.Sin (Mathf.Deg2Rad * appliedPitch) * Mathf.Sin (Mathf.Deg2Rad * appliedYaw);
			float Y = Mathf.Cos (Mathf.Deg2Rad * appliedPitch);

			//Modify campera position
			cameraPosition = new Vector3 (X, Y, Z) * distanceFromTarget; 
			controlledView.TransposerTrackingOffset = cameraPosition;
		}

	}

	
	public void increaseYaw(float amt)
	{
		if(modifiable)
			yaw = (yaw + amt) % 360;

	}

	public void increasePitch(float amt)
	{
		if(modifiable)
			pitch = (pitch + amt) % 360;
	}


}
