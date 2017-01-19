using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : AbCamera {


	//TODO Ease camera tracking movements.
	//TODO Implement camera movement tracking into methods which can also be used when wanting to manually rotate the camera.

	//private Camera trackingCamera;
	public GameObject target;

	//
	// Target Tracking Information
	//
	private Vector3 targetCurrentPosition;
	private Vector2 targetCurrentRotation;
	public Vector2 targetTrackedRotation;

	//
	// Camera Tracking Information
	//
	public bool trackPosition = true;
	public bool trackRotation = false;
	public bool lookAt = true;


	public float distanceFromTarget = 0;
	public Vector2 angleFromTarget = Vector2.zero;


	//Current rotation In degrees (TODO make into properties and use accessor methods to clamp them?)
	private float pitch = 0;
	private float yaw = 0;

	private Vector3 cameraPosition = Vector3.zero;

	public float minXAngke = -90;
	public float maxXAngle = 90;
		

	void Start()
	{
		if (target == null) {
			target = GameObject.FindGameObjectWithTag ("Player");
		}
	}
	public override Vector3 getPosition()
	{
		targetCurrentPosition = target.transform.position;
		targetCurrentRotation = new Vector2(target.transform.rotation.eulerAngles.z,target.transform.rotation.eulerAngles.y);

		float appliedYaw = yaw + angleFromTarget.y;
		float appliedPitch = pitch + angleFromTarget.x;

		if (trackRotation)
		{
			Vector2 rotationDifference = targetCurrentRotation - targetTrackedRotation;
			appliedPitch -= rotationDifference.x;
			appliedYaw -= rotationDifference.y;
		}

		pitch = Mathf.Clamp (pitch, minXAngke, maxXAngle);

		//Polar to cartesian conversion
		float X = Mathf.Sin (Mathf.Deg2Rad * appliedPitch) * Mathf.Cos (Mathf.Deg2Rad * appliedYaw);
		float Z = Mathf.Sin (Mathf.Deg2Rad * appliedPitch) * Mathf.Sin (Mathf.Deg2Rad * appliedYaw);
		float Y = Mathf.Cos (Mathf.Deg2Rad * appliedPitch);

		//Modify campera position
		cameraPosition = new Vector3 (X, Y, Z) * distanceFromTarget; 
		if (trackPosition) {      
			this.transform.position = cameraPosition + targetCurrentPosition;
		}

		return this.transform.position;
	}
		
	public override Vector3 getTarget()
	{
		if (lookAt) {
			//Point camera at target
			this.transform.LookAt (targetCurrentPosition);
		}

		return targetCurrentPosition;
	}
		

}
	