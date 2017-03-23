using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIHUDManager : MonoBehaviour {


	#region Button Icons

	[SerializeField]
	private Image _AButtonImage;
	[SerializeField]
	private Image _BButtonImage;
	[SerializeField]
	private Image _XButtonImage;
	[SerializeField]
	private Image _YButtonImage;

	#endregion


	#region Bird HUD Icons
	/// <summary>
	/// Transform containing all icons for bird abilities
	/// </summary>
	[SerializeField]
	private Image _BirdSwoopIcon;
	[SerializeField]
	private Image _BirdPickDropIcon;
	[SerializeField]
	private Image _BirdDrawIcon;

	#endregion

	#region Bear HUD Icons

	/// <summary>
	/// Transform containing all icons for bear abilities
	/// </summary>
	[SerializeField]
	private Image _BearInteractIcon;
	[SerializeField]
	private Image _BearThrowTagIcon;
	[SerializeField]
	private Image _BearRoarIcon;

	#endregion 


	#region Current Player Images
	/// <summary>
	/// Image showing the player of this system is the Bear character
	/// </summary>
	[SerializeField]
	private Image _CurrentPlayerBearImage;

	/// <summary>
	/// Image showing the player of this system is the Bird character
	/// </summary>
	[SerializeField]
	private Image _CurrentPlayerBirdImage;

	#endregion


	// Use this for initialization
	void Start () {

		showPlayerCharacterIcon ();

		// Set all button images to inactive at start
		//deactivateButtonImages();

		// Set all ability icons to inactive at start
		//deactivateAbilityImages ();

	}

	//TODO: Testing Method
	private void handleInput()
	{
		if (Input.GetKeyDown (KeyCode.Alpha7)) 
		{
			revealButton ("A");
		}
		if (Input.GetKeyDown (KeyCode.Alpha8)) 
		{
			revealButton ("B");
		}
		if (Input.GetKeyDown (KeyCode.Alpha9)) 
		{
			revealButton ("X");
		}
		if (Input.GetKeyDown (KeyCode.Alpha0)) 
		{
			revealButton ("Y");
		}
	}

	/// <summary>
	/// Shows the ability icon whenever player is within a context where icon
	/// should be displayed
	/// </summary>
	/// <param name="p_Button">P button.</param>
	public void showAbility(string p_AbilityName)
	{
		switch (p_AbilityName) 
		{
		case "Bird-Swoop":
			//do something
			break;
		case "Bird-Pick/Drop":
			//do something
			break;
		case "Bird-Draw":
			//do something
			break;
		case "Bear-Interact":
			_BearInteractIcon.gameObject.SetActive (true);
			_BearThrowTagIcon.gameObject.SetActive (false);
			break;
		case "Bear-ThrowTag":
			_BearInteractIcon.gameObject.SetActive (false);
			_BearThrowTagIcon.gameObject.SetActive (true);
			break;
		case "Bear-Roar":
			//do something
			break;
		default:
			break;
		}
	}

	/// <summary>
	/// Reveals the ability for the first time player ever enters a context 
	/// where this ability is able to be performed
	/// </summary>
	public void revealButton(string p_Button)
	{
		switch (p_Button) 
		{
		case "A":
			_AButtonImage.gameObject.SetActive (true);
			break;
		case "B":
			_BButtonImage.gameObject.SetActive (true);
			break;
		case "X":
			_XButtonImage.gameObject.SetActive (true);
			break;
		case "Y":
			_YButtonImage.gameObject.SetActive (true);
			break;
		default:
			break;
		}
	}


	/// <summary>
	/// Shows the player character icon based on if server or if client
	/// </summary>
	private void showPlayerCharacterIcon()
	{
		//TODO if (isServer)
		//{
			//_CurrentPlayerBearImage.gameObject.SetActive(true);
			//_CurrentPlayerBirdImage.gameObject.SetActive(false);
		//}

		//TODO if (isClient)
		//{
			//_CurrentPlayerBirdImage.gameObject.SetActive(true);
			//_CurrentPlayerBearImage.gameObject.SetActive(false);
		//}
	}

	/// <summary>
	/// Deactivates the button images for HUD
	/// </summary>
	private void deactivateButtonImages()
	{
		_AButtonImage.gameObject.SetActive (false);
		_BButtonImage.gameObject.SetActive (false);
		_XButtonImage.gameObject.SetActive (false);
		_YButtonImage.gameObject.SetActive (false);

	}

	/// <summary>
	/// Deactivates the ability images for HUD
	/// </summary>
	private void deactivateAbilityImages()
	{
		_BearInteractIcon.gameObject.SetActive (false);
		_BearRoarIcon.gameObject.SetActive (false);
		_BearThrowTagIcon.gameObject.SetActive (false);

		_BirdDrawIcon.gameObject.SetActive (false);
		_BirdPickDropIcon.gameObject.SetActive (false);
		_BirdSwoopIcon.gameObject.SetActive (false);

	}
	// Update is called once per frame
	void Update () {
		handleInput ();
	}
}
