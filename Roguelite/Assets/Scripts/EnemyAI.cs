using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

	public int currentHealth;
	public int maxHealth;

	public float moveSpeed;
	//checks whether the enemy is moving or not, for how long, and for how long to wait
	public float timeBetweenMove;
	private float timeBetweenMoveCounter;
	public float timeToMove;
	private float timeToMoveCounter;

	private Rigidbody2D myrigid;
	public Animator anim;

	//checks whether the enemy moving is true or false
	private bool moving;

	private Vector3 moveDirection;

	void Start()
	{
		//at spawn, the enemy's current health is whatever it's maximum health is.
		currentHealth = maxHealth;

		myrigid = GetComponent<Rigidbody2D>();

		//timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
		//timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

		timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
		timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeBetweenMove * 1.25f);

	}
	void Update()
	{
		//the move counter counts down, and upon zero the enemy moves in a random direction
		//once the enemy is done moving, the counter begins once more
		if (moving)
		{
			timeToMoveCounter -= Time.deltaTime;
			myrigid.velocity = moveDirection;

			if (timeToMoveCounter < 0f)
			{
				moving = false;
				timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
			}
		}
		else
		{
			timeBetweenMoveCounter -= Time.deltaTime;
			myrigid.velocity = Vector2.zero;
			if (timeBetweenMoveCounter < 0f)
			{
				moving = true;
				timeToMoveCounter = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

				moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f);
			}

		}

		//if the enemy's health is equal to OR less than zero, it will get destroyed- ie die
		if (currentHealth <= 0) 
		{
			Destroy (gameObject);
		}
			
	}

	//for taking damage, the value of which is in the Attacking script
	public void Damage (int damage)
	{
		currentHealth -= damage;
		// the line to play an animation of the enemy getting hit is here: gameObject.GetComponent<Animation> ().Play ("Hit");
		//...I just haven't made the animation yet orz
	}
}﻿