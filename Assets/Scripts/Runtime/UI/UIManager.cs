using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	#region Lobby Panel Attributes

	/// <summary>
	/// The lobby panel transform
	/// </summary>
	[SerializeField]
	private Transform _LobbyPanel;

	#endregion


	#region HUD Panel Attributes

	[SerializeField]
	private Transform _HUDBottomPanel;

	#endregion

	#region Pause Panel Attributes
	/// <summary>
	/// The pause panel transform
	/// </summary>
	[SerializeField]
	private Transform _PausePanel;
	private Animator _PausePanelAnimator;


	/// <summary>
	/// Boolean preventing display of pause menu during lobby screen
	/// </summary>
	public static bool canPause;

	#endregion

	#region Dialogue Panel Attributes
	/// <summary>
	/// The dialogue panel transform
	/// </summary>
	[SerializeField]
	private Transform _DialoguePanel;

	#endregion

	// Use this for initialization
	void Start () {

		//_LobbyPanel.gameObject.SetActive (true);
		//_LobbyPanel.GetComponent<UILobbyManager> ().startLobbyScreen ();

		setPausePanel ();
		hideDialoguePanel ();
	}


	private void handleInput()
	{
		if (Input.GetKeyDown (KeyCode.K)) 
		{
			pauseGame ();
		}

		if (Input.GetKeyDown (KeyCode.P)) 
		{
			unpauseGame ();
		}
	}


	private void hideDialoguePanel()
	{
		_DialoguePanel.gameObject.SetActive (true);
		Animator _DialogueAnimator = _DialoguePanel.GetComponent<Animator> ();
		_DialogueAnimator.Play ("HideDialogue");
	}

	private void setPausePanel()
	{
		// Set all animation values for the Pressed animation for smooth transitioning
		_PausePanelAnimator = _PausePanel.GetComponent<Animator> ();

		// Set Pause Panel to false at start
		_PausePanel.gameObject.SetActive (false);
	}

	/// <summary>
	/// Handles display of Pause Menu when the game is in a paused state
	/// </summary>
	public void pauseGame()
	{
		if (canPause) 
		{
			_PausePanel.gameObject.SetActive (true);
			_PausePanel.GetComponent<Animator> ().Play ("OpenAnimation");
		}
	}

	/// <summary>
	/// Handles closing of pause menu when the game is set to an unpaused state
	/// </summary>
	public void unpauseGame()
	{
		_PausePanel.GetComponent<Animator> ().Play ("CloseAnimation");
	}
		

	/// <summary>
	/// Starts the Lobby screen when called
	/// </summary>
	public void openLobby()
	{
		canPause = false;

		hideDialoguePanel ();
		unpauseGame ();

		_LobbyPanel.gameObject.SetActive (true);
		_LobbyPanel.GetComponent<UILobbyManager> ().startLobbyScreen ();
	}

	// Update is called once per frame
	void Update () {
	
		//TODO: remove this method when finished
		handleInput();
	}
}
