using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

	public float moveSpeed;
	//checks whether the enemy is moving or not, for how long, and for how long to wait
	public float timeBetweenMove;
	private float timeBetweenMoveCounter;
	public float timeToMove;
	private float timeToMoveCounter;

	private Rigidbody2D myrigid;

	//checks whether the enemy moving is true or false
	private bool moving;

	private Vector3 moveDirection;

	void Start()
	{
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
	}
}﻿