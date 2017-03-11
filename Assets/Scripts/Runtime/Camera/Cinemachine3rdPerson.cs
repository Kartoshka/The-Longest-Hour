using UnityEngine;
using System.Collections;
using Cinemachine.Utility;
using Cinemachine.Attributes;
using Cinemachine.Blending;
using Cinemachine.Assets;
using Cinemachine;


public class Cinemachine3rdPerson :MonoBehaviour  {


	public bool modifiable = true;
	public bool setOnStart = true;
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
	public float pitchChangeSpeed = 5f;
	public float yawChangeSpeed = 5f;

	private Vector3 cameraPosition = Vector3.zero;

	public float minXAngle = -90;
	public float maxXAngle = 90;


	//Collision parameters
	[Header("Collision Properties")]

	public bool fixCollisions = false;
	public LayerMask collisionLayers;
	public float radiusCollision = 0.2f;
	private RaycastHit hitCamera;

	//When the camera collides and zooms in, it will set itself to this factor (%) between the target and where the collision happened
	[Range(0,1)]
	public float zoomFactor =0.5f;

	void Start()
	{
		if (setOnStart) {
			setPosition ();
			if (fixCollisions) {
				collisionOffset (cameraPosition + controlledView.CameraTransposerTarget.position);
			}

			controlledView.TransposerTrackingOffset = cameraPosition;
		}
	}

	public void UpdatePosition()
	{
		setPosition ();
		if (fixCollisions) {
			collisionOffset (cameraPosition + controlledView.CameraTransposerTarget.position);
		}

		controlledView.TransposerTrackingOffset = cameraPosition;


	}

	private void setPosition()
	{
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

	}

	Vector3 directionRay;
	Vector3 startRay;



	private void collisionOffset(Vector3 startPosition){
		 directionRay = (controlledView.CameraTransposerTarget.position - controlledView.CameraPosition);
		 startRay = startPosition;

		RaycastHit[] hits   = Physics.SphereCastAll(startRay, radiusCollision, directionRay, distanceFromTarget,collisionLayers);
		hitCamera.distance  = Mathf.Infinity;
		foreach (RaycastHit hit in hits)
		{
			if (hit.distance < hitCamera.distance)
			{
				hitCamera = hit;
				if (hit.point == Vector3.zero) {
					hitCamera.point = startRay - directionRay.normalized * radiusCollision * 5f;
				}
			}
		}

		if (hitCamera.distance != Mathf.Infinity) {
			float distance = Vector3.Distance (hitCamera.point,controlledView.CameraTransposerTarget.position);
			cameraPosition = cameraPosition.normalized * (distance*zoomFactor);
		}
			
	}

	public void increaseYaw(float amt)
	{
		if (modifiable) {
			yaw = (yaw + amt*yawChangeSpeed*Time.deltaTime) % 360;
		}

	}

	public void increasePitch(float amt)
	{
		if (modifiable) {
			pitch = (pitch + amt*pitchChangeSpeed*Time.deltaTime) % 360;
		}
	}


}
