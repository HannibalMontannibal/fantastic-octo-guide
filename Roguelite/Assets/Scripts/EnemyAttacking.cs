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
        

    }
	void OnTriggerEnter2d(Collider2D other)
	{
		if (!other.CompareTag ("Player")) {
			//checks distance between enemy and player, seeing if the player is close enough to attack
			float distanceToPlayer = Vector3.Distance (transform.position, target.position);
			if (distanceToPlayer < attackRange) {
				//checks to see if enough time has passed since the last attack. Only does attack if enough time has passed.
				if (Time.time > lastAttackTime + attackDelay) {
					target.SendMessage ("Damage", damage, SendMessageOptions.DontRequireReceiver);
					//Records the time the enemy last attacked
					lastAttackTime = Time.time;
				}
			}
		}
	}
}
