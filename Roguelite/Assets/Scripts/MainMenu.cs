using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	//plays the game when you press the button.
	public void ToGame()
	{
		SceneManager.LoadScene ("MainGame");
	}
}
