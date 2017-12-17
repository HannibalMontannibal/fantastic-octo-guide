using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {
	public bool isPaused;

	public GameObject pauseMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//pauses the game and brings up the menu when escape has been hit.
		//also unpauses the menu when 'Escape' is hit a second time.
		if (isPaused) {
			pauseMenu.SetActive (true);
			Time.timeScale = 0f;
			Cursor.visible = true;
		} else 
		{
			pauseMenu.SetActive (false);
			Time.timeScale = 1f;
			Cursor.visible = false;

		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			isPaused = !isPaused;
		}
	}

	public void QuitGame()
	{
		//Quits the game when Built
		//Debug.Log ("Game has been quitted");
		Application.Quit ();
	}

	public void Continue()
	{
		isPaused = false;
	}
}
