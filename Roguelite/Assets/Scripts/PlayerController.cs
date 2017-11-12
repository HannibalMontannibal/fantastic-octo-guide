﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	//determines the player's movement speed
	public float moveSpeed;


	//private Rigidbody2D rb2d;
	// Use this for initialization
	void Start()
	{
		//{
		//	rb2d = GetComponent<Rigidbody2D> ();
		//}
	}

	//void OnCollisionEnter(Collision collision) 
	//{
		//if(gameObject.CompareTag("Wall"))
		//{
		//	rb2d.velocity = Vector3.zero;
		//}
	//}
	
	// Update is called once per frame
	void Update () 
	{
		//checks if our input on the x axis is moving the player right OR (||) left (if the player is moving the character to the right)
		if (Input.GetAxisRaw ("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < 0.5f) 
		{
			transform.Translate (new Vector3 (Input.GetAxisRaw ("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
		}

		//checks if the input on the y axis is moving the player up or down

		if (Input.GetAxisRaw ("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < 0.5f) 
		{
			transform.Translate (new Vector3 (0f, Input.GetAxisRaw ("Vertical") * moveSpeed * Time.deltaTime, 0f));
		}
	}
}