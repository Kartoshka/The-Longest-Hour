using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {


	//TODO Ease camera tracking movements.
	//TODO Implement camera movement tracking into methods which can also be used when wanting to manually rotate the camera.

	private Camera trackingCamera;
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

	//
	// Rotation coroutine attributes
	//

	private Coroutine rotationCoroutine;
	Vector3 rotationVelocity = new Vector3 ();


	public float minXAngke = -90;
	public float maxXAngle = 90;


	void Start () {
		trackingCamera = this.GetComponent<Camera>();

		pitch = angleFromTarget.x;
		yaw = angleFromTarget.y;
	}
		
	void Update () {

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
			trackingCamera.transform.position = cameraPosition + targetCurrentPosition;
		}

		if (lookAt) {
			//Point camera at target
			trackingCamera.transform.LookAt (targetCurrentPosition);
		}
	}
	
	/*
	 * Change zoom radius of camera in a given amount of time
	 */
	IEnumerator lerpRadiusTime(float finalRadius, float zoomTime)
	{
		
		yield return null;
	}
		
	IEnumerator lerpRadiusSpeed(float finalRadius, float speed)
	{
		yield return null;
	}

	/*
	 * Change view angle of camera in a given amount of time
	 */
	IEnumerator lerpRotationTime(Vector2 finalRotation, float rotationTime)
	{

		while (Vector2.Distance(finalRotation,new Vector2(pitch,yaw))>1)
		{
			//Investigate this function, can definitely be used!
			Vector3 result = Vector3.SmoothDamp (new Vector3 (pitch, yaw, 0), finalRotation, ref rotationVelocity, rotationTime,float.MaxValue,Time.deltaTime);
			pitch = result.x;
			yaw = result.y;
			yield return new WaitForEndOfFrame ();
		}
		rotationVelocity = Vector2.zero;
		yield return null;
	}


	/*
	 * Change view angle of camera with a given speed per degree of rotation
	 */
	IEnumerator lerpRotationSpeed(Vector2 finalRotation, float speed)
	{
		float timeToTravel = (new Vector2 (pitch, yaw) - finalRotation).magnitude / speed ;
		return lerpRotationTime (finalRotation, timeToTravel);
	
	}

	private void stopRotation()
	{
		if (rotationCoroutine != null) {
			StopCoroutine (rotationCoroutine);
			rotationCoroutine = null;
			rotationVelocity = Vector3.zero;
		}
	}
}
	