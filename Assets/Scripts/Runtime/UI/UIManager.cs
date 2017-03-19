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
	private Transform _HUDPanel;

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

	private UIDialogueManager _DialogueManager;

	#endregion

	// Use this for initialization
	void Start () {

		openLobbyFirst ();

		setPausePanel ();
		setDialoguePanel ();
	}


	/// <summary>
	/// Handles Player input
	/// </summary>
	private void handleInput()
	{

		// TODO: Get proper Player input to listen for
		if (Input.GetKeyDown (KeyCode.K)) 
		{
			pauseGame ();
		}

		// TODO : Get proper player input to listen for
		if (Input.GetKeyDown (KeyCode.P)) 
		{
			unpauseGame ();
		}
	}


	/// <summary>
	/// Sets the dialogue panel and hides it on start
	/// </summary>
	private void setDialoguePanel()
	{
		_DialoguePanel.gameObject.SetActive (true);
		_DialogueManager = _DialoguePanel.GetComponent<UIDialogueManager> ();

		Animator _DialogueAnimator = _DialoguePanel.GetComponent<Animator> ();
		_DialogueAnimator.Play ("HideDialogue");
	}

	/// <summary>
	/// Sets the pause panel and hides it on start
	/// </summary>
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
	/// Plays through dialogue contained in parameter p_Lines
	/// </summary>
	/// <param name="p_Lines">P lines.</param>
	public void playDialogue(List<UIDialogueLine> p_Lines)
	{
		_DialogueManager.showDialogue (p_Lines);	
	}

	/// <summary>
	/// Starts the Lobby screen when called
	/// </summary>
	public void openLobby()
	{
		canPause = false;

		setDialoguePanel ();
		unpauseGame ();

		_LobbyPanel.gameObject.SetActive (true);
		_LobbyPanel.GetComponent<UILobbyManager> ().startLobbyScreen ();
	}

	private void openLobbyFirst()
	{
		canPause = false;

		_LobbyPanel.gameObject.SetActive (true);
		_LobbyPanel.GetComponent<UILobbyManager> ().startLobbyScreen ();
	}

	// Update is called once per frame
	void Update () {
	
		//TODO: remove this method when finished
		handleInput();
	}
}
