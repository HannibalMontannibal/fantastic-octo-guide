using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingTrigger : MonoBehaviour {

	//the damage dealt when attacking an enemy
	public int dmg = 20;

	void OnTriggerEnter2D(Collider2D col)
	{
		//calls a function with whatever is being collided with- ie the enemy
		//if whatever is hit has the tag "Enemy", then the damage function will be called, causing the enemy to take damage

		if (col.isTrigger != true && col.CompareTag ("Enemy"))
		{
			col.SendMessageUpwards ("Damage", dmg);
		}
	}
}
