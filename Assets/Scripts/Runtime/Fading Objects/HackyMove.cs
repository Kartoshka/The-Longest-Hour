using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FadingObjects;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
public class HackyMove : TimeBasedObjects {

	public Color invalidColor = Color.red;
	public Color validColor = Color.white;


	public FadingObject spawnedItem;
	float m_TurnAmount;
	Vector3 m_ForwardAmount;
	private Material mat;

	public Timer time;
	public Text subtractText;

	private float lifeTimeSpawned = 0;
	private bool spawning;

	private List<GameObject> collidingWith;

	public void Move(Vector2 move)
	{
		this.gameObject.transform.position += new Vector3 (move.x, 0, move.y);
	}

	protected override void initialize ()
	{
		collidingWith = new List<GameObject> ();
		this.gameObject.SetActive (false);
		mat = this.gameObject.GetComponent<MeshRenderer> ().material;
		mat.SetColor ("_Color", validColor);

	}

	protected override void OnPause ()
	{
		this.gameObject.SetActive (true);
	}

	protected override void OnResume ()
	{
		this.gameObject.SetActive (false);
	}

	protected override void PausedUpdate ()
	{
		float delta = GlobalTimeManager.getDeltaTime ();

		if ((lifeTimeSpawned + delta) < time.getTimeLeft()) {
			lifeTimeSpawned += delta;
		}

		subtractText.text = "-"+Timer.formatTime (lifeTimeSpawned);
	}

	protected override void RunningUpdate ()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		mat.SetColor ("_Color", invalidColor);
		collidingWith.Add (other.gameObject);
	}

	void OnTriggerExit(Collider other)
	{
		if (collidingWith.Remove (other.gameObject)) {
			if (collidingWith.Count == 0) {
				mat.SetColor ("_Color", validColor);
			}
		}
	}

	public void startCountDown()
	{
		subtractText.gameObject.SetActive (true);
		spawning = true;
	}

	public void endCountDown()
	{
		if (lifeTimeSpawned > 0) {
			time.removeTime (lifeTimeSpawned);
			GameObject _spawned = (GameObject)Instantiate (spawnedItem.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation);
			_spawned.GetComponent<FadingObject> ().duration = lifeTimeSpawned;
		}
		lifeTimeSpawned = 0;
		subtractText.gameObject.SetActive (false);
		spawning = false;
	}


}
