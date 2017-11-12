using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float speed;

	public Transform target;
	//how close the player needs to be in order for the enemy to start chasing
	public float chaseRange;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//gets the distance of the target and checks to see if it is close enough to chase
		float distanceToTarget = Vector3.Distance (transform.position, target.position);
		if (distanceToTarget < chaseRange) 
		{
		//start chasing the target- turns and moves to target
			Vector3 targetDir = target.position = transform.position;
			float angle = Mathf.Atan2 (targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, q, 180);

			//moves the enemy
			transform.Translate (Vector3.up * Time.deltaTime * speed);
		}
	}
}
