using System.Collections;
//so the List can be used
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour {

	//the [] shows that this variable is going to be an array
	public GameObject[] tiles;

	public GameObject wall;

	//lists are more dynamic than the other array
	public List<Vector3> createdTiles;

	//the amount of tiles that the generator is going to be generating
	public int tileAmount;

	//the space between each movement of the generator- which is going to be the size of the tiles
	public int tileSize;

    public float waitTime;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (GenerateLevel ());
	}

	IEnumerator GenerateLevel()
	{
		//for each tile created, we are moving the generator in one of the directions of the (0, 4) range- 0 being up, 1 being right, 2 being left and 3 being down
		for (int i = 0; i < tileAmount; i++) 
		{
			int dir = Random.Range(0, 4);

			MoveGen(dir);

            //slows down the generating process slightly, so we can see what's happening
            yield return new WaitForSeconds(waitTime);
        }

       //fixes an error
        yield return 0;
	}
	//makes the generator move- 'dir' indicating direction 
	void MoveGen(int dir)
	{
		//am using switch() to set up a lot of if statements quickly
		//the cases represent the range (up, down, left, right)
		switch (dir) 
		{
		case 0: 

			transform.position = new Vector3 (transform.position.x, transform.position.y + tileSize, 0); 

			break;

		case 1:
                transform.position = new Vector3(transform.position.x + tileSize, transform.position.y, 0);

                break;

		case 2:
                transform.position = new Vector3(transform.position.x + tileSize, transform.position.y - tileSize, 0);

                break;

		case 3:
                transform.position = new Vector3(transform.position.x - tileSize, transform.position.y, 0);

                break;

		}
	}
}
