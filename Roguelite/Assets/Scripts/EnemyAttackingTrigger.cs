using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingTrigger : MonoBehaviour
{
    //the damage dealt when attacking the player
    public int dmg = 30;

    void OnTriggerEnter2D(Collider2D col)
    {
        //calls a function with whatever is being collided with- ie the player
        //if whatever is hit has the tag "Player", then the damage function will be called, causing the player to take damage

       if (col.isTrigger != true && col.CompareTag("Player"))
      {
			col.SendMessageUpwards("Damage", dmg, SendMessageOptions.DontRequireReceiver);
        }
    }
}
