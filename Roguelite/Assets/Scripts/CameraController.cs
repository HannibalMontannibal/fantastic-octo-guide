using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//follows whatever it put as 'Target', i.e. the player
	public GameObject followTarget;


	//gives a position of the camera to aim towards the Target
	private Vector3 targetPos;

	//how fast the camera chases the player
	public float moveSpeed;

	// Use this for initialization
	void Start () 
	{
		//followTarget = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		//locates where the player is
		targetPos = new Vector3 (followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);	
		//moves camera to that position
		transform.position = Vector3.Lerp (transform.position, targetPos, moveSpeed * Time.deltaTime);

		followTarget = GameObject.FindWithTag("Player");
	}
}
