using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacking : MonoBehaviour
{
    public Transform target;

    //how close the player needs to be for the enemy to attack
    public float attackRange;
    public int damage;

    //determines the time being delayed between attacks
    private float lastAttackTime;
    public float attackDelay;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //checks distance between enemy and player, seeing if the player is close enough to attack
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (distanceToPlayer < attackRange)
        {
           
        }

    }
}
