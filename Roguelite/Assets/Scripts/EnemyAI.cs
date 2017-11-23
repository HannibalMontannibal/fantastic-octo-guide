using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public Transform target;

	//how close the player needs to be in order for the enemy to start chasing
	public float chaseRange;
	public float stopRange = 20f;

	public int currentHealth;
	public int maxHealth;

	public float moveSpeed;
	//checks whether the enemy is moving or not, for how long, and for how long to wait
	public float timeBetweenMove;
	private float timeBetweenMoveCounter;
	public float timeToMove;
	private float timeToMoveCounter;


	//how close the player needs to be for the enemy to attack
	public float attackRange;
	public int damage;

	//determines the time being delayed between attacks
	private float lastAttackTime;
	public float attackDelay;


	private Rigidbody2D myrigid;
	private Animator anim;

	//checks whether the enemy moving is true or false
	private bool moving;
	private bool withinAttackRange;

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

		anim = gameObject.GetComponent<Animator> ();

	}
	void Update()
	{
		//gets the distance of the target and checks to see if it is close enough to chase
		float distanceToTarget = Vector3.Distance (transform.position, target.position);

		if (withinAttackRange) {
			//checks distance between enemy and player, seeing if the player is close enough to attack
			//float distanceToPlayer = Vector3.Distance (transform.position, target.position);
			//if (distanceToPlayer < attackRange) {
			//checks to see if enough time has passed since the last attack. Only does attack if enough time has passed.
			if (Time.time > lastAttackTime + attackDelay) {
				target.gameObject.SendMessage ("TakeDamage", damage);
				//, SendMessageOptions.DontRequireReceiver
				//Records the time the enemy last attacked
				lastAttackTime = Time.time;
			}
			//}
		}

		if (distanceToTarget < stopRange) {
			myrigid.velocity = Vector2.zero;
		}
		else if (distanceToTarget < chaseRange) {
			//start chasing the target- turns and moves to target
			Vector3 targetDir = target.position - transform.position;
			//float angle = Mathf.Atan2 (targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
			//Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			//transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 180);

			myrigid.velocity = targetDir.normalized * moveSpeed;

			//moves the enemy
//			transform.Translate (Vector3.up * Time.deltaTime * moveSpeed);
		} else {

			//the move counter counts down, and upon zero the enemy moves in a random direction
			//once the enemy is done moving, the counter begins once more
			if (moving)
			{
				timeToMoveCounter -= Time.deltaTime;
				myrigid.velocity = moveDirection;
				if (timeToMoveCounter < 0f) {
					moving = false;
					anim.SetBool ("Idling", moving);
					timeBetweenMoveCounter = Random.Range (timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
				}
			} else {
				timeBetweenMoveCounter -= Time.deltaTime;
				myrigid.velocity = Vector2.zero;
				if (timeBetweenMoveCounter < 0f) {
					moving = true;
					anim.SetBool ("Walking", moving);
					timeToMoveCounter = Random.Range (timeToMove * 0.75f, timeToMove * 1.25f);

					moveDirection = new Vector3 (Random.Range (-1f, 1f) * moveSpeed, Random.Range (-1f, 1f) * moveSpeed, 0f);
				}
			}
		}

		//if the enemy's health is equal to OR less than zero, it will get destroyed- ie die
		if (currentHealth <= 0) 
		{
			Destroy (gameObject);
		}
			
		if (myrigid.velocity.x > 0.001f) {
			transform.localScale = new Vector3 (1, 1, 1);
		}
		else if (myrigid.velocity.x < -0.001f) {
			transform.localScale = new Vector3 (-1, 1, 1);
		}

    
	}

	//for taking damage, the value of which is in the Attacking script
	public void Damage (int damage)
	{
		currentHealth -= damage;
		gameObject.GetComponent<Animation> ().Play ("Hurt");
		//...I just haven't made the animation yet orz
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			withinAttackRange = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag ("Player")) {
			withinAttackRange = false;
		}
	}
}﻿