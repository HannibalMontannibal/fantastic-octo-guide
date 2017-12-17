using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController control;


	void Awake ()
		{
	//		//if there is no other game object of this type (level generator) this allows for this specific game object to
	//		//be the only one throughout all scenes
			if (control == null) {
				DontDestroyOnLoad (gameObject);
				control = this;
			}

	//if one already exists, destroy this copy of it so as to avoid duplicates
		else if (control != this) 
		{
			Destroy (gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
