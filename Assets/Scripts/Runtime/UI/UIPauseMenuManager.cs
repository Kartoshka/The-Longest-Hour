using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UIPauseMenuManager : MonoBehaviour {

	[SerializeField]
	private EventSystem _EventSystem;

	[SerializeField]
	private GameObject _SelectedGameObject;

	/// <summary>
	/// The continue button in the Pause Panel
	/// </summary>
	[SerializeField]
	private Button _ContinueButton;

	/// <summary>
	/// The exit button in the Pause Panel
	/// </summary>
	[SerializeField]
	private Button _ExitButton;

	[SerializeField]
	private bool _ButtonSelected;

	// Use this for initialization
	void Start () {


	}

	private void handleInput()
	{
		if (Input.GetAxisRaw ("Vertical") != 0 && _ButtonSelected == false) 
		{

			_EventSystem.SetSelectedGameObject (_SelectedGameObject);

			Debug.Log ("Vertical Value Changed");
			_ButtonSelected = true;
		}
	}
		

	private void OnDisable()
	{
		_ButtonSelected = false;
	}

	// Update is called once per frame
	void Update () {

		handleInput ();
	}
}
