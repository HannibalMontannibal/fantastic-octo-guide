﻿using System.Collections;
//so the List can be used
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour 
{
	public GameObject gameOverText;
	//public bool playerDead = false;
	public bool gameOver = false;

	public GameObject player;
	public GameObject enemy;
	public GameObject camera;
	public GameObject portal;

	public Text scoreText;
	public Text highScore;
	public Text enemyCount;
	public Text healthText;

	private int score = 0;
	public int enemyAmount = 15;

	//the [] shows that this variable is going to be an array
	public GameObject[] tiles;

	public GameObject wall;

	//lists are more dynamic than the other array
	public List<Vector3> createdTiles;

	//the amount of tiles that the generator is going to be generating
	public int tileAmount;

	//the space between each movement of the generator- which is going to be the size of the tiles
	public float tileSize;

   

   public float chanceUp;
   public float chanceRight;
   public float chanceDown;

    public float waitTime;

	//wall generation
	//indicates the lowest floor on the x and y axis (min) as well as the highest for both (max)
	public float minY = 9999999999;
	public float maxY = 0;
	public float minX = 9999999999;
	public float maxX = 0;
	//the amount of walls on the x and y axis
	public float xAmount;
	public float yAmount;
	public float extraWallX;
	public float extraWallY;


	public GameObject mainCamera;

	// Use this for initialization
	void Start ()
	{


		StartCoroutine (GenerateLevel ());

        //To make a specific seed:  (the specific number in place of 'x' is the same level)
        //Random.seed = 10;


		int hs = GetHighScore();
		highScore.text = "High Score: " + hs.ToString ();
	}

	void Update()
	{
		//reloads the game when spacebar is hit, provided that the player is dead
		if (gameOver == true && Input.GetKeyDown (KeyCode.Space)) 
		{
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}

		//makes the cursor invisible
			Cursor.visible = false;
		

	}

	int GetHighScore()
	{
		return PlayerPrefs.GetInt ("HighScore", 0);
	}

	IEnumerator GenerateLevel()
	{
		//for each tile created, we are moving the generator in one of the directions of the (0, 4) range- 0 being up, 1 being right, 2 being left and 3 being down
		for (int i = 0; i < tileAmount; i++) 
		{
			float dir = Random.Range(0f, 1f);
            int tile = Random.Range(0, tiles.Length);

            CreateTile(tile);
            CallMoveGen(dir);

            //slows down the generating process slightly, so we can see what's happening
            yield return new WaitForSeconds(waitTime);

			//checks if floor generation is done, so it can then spawn in walls (calls WallGen)
			//checks if i is right on the tile amount, so it gets called only once the floor generation is finished
			if(i == tileAmount -1)
			{
				Finish ();
			}

        }

       //fixes an error
        yield return 0;
	}

    //changes the chances of the direction in which the level generator moves
    //for example, if chanceRight has the highest odds, it's more likely the level will lean to the right rather than the left
    void CallMoveGen(float ramDir)
    {
        if (ramDir < chanceUp)
        {
            MoveGen(0);
        }
        else if (ramDir < chanceRight)
        {
            MoveGen(1);
        }
        else if (ramDir < chanceDown)
        {
            MoveGen(2);
        }

        else
        {
            MoveGen(3);
        }
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
			transform.position = new Vector3(transform.position.x, transform.position.y - tileSize, 0);

                break;

		case 3:
                transform.position = new Vector3(transform.position.x - tileSize, transform.position.y, 0);

                break;

		}
	}

    //spawns tiles 
    void CreateTile(int tileIndex)
    {
        //checks if it DOESN'T contain the current position of the generator
        if (!createdTiles.Contains(transform.position))

        {
            GameObject tileObject;

            tileObject = Instantiate(tiles[tileIndex], transform.position, transform.rotation) as GameObject;

            createdTiles.Add(tileObject.transform.position);
        }
        else
        {
            tileAmount++;
        }

    }
	//for when the floors are done spawning in
	//calls in two functions to make the walls
	void Finish()
	{
		CreateWallValues ();
		CreateWalls ();
		SpawnObjects ();

	}
		//spawns player and enemies
	void SpawnObjects()
	{
		GameObject playerInstance = Instantiate(player, createdTiles[Random.Range(0, createdTiles.Count)], Quaternion.identity);
		PlayerController pc = playerInstance.GetComponent<PlayerController> ();
		pc.levelGen = this;
		pc.healthText = healthText;

		// Instantiate & configure camera
		GameObject cameraInstance = Instantiate(camera, createdTiles[Random.Range(0, createdTiles.Count)], Quaternion.identity);
		cameraInstance.transform.Translate (0, 0, -10);
		CameraController cc = cameraInstance.GetComponent<CameraController> ();
		cc.followTarget = playerInstance;

		//levelLoaded = true;
		for (int i = 0; i < enemyAmount; i++) 
		{
			//spawns an enemy on a random tile
			GameObject enemyInstance = Instantiate(enemy, createdTiles[Random.Range(0, createdTiles.Count)], Quaternion.identity);
			EnemyAI eai = enemyInstance.GetComponent<EnemyAI> ();
			eai.target = playerInstance.transform;
			eai.levelGenerator = this;
		}
	}

	void CreateWallValues()
	{
		//checks for the value of the lowest floor piece on the Y axis, then the highest for the Y axis
		//does the same for the values on the X axis, too
		for (int i = 0; i < createdTiles.Count; i++) 
		{
			if (createdTiles [i].y < minY) 
			{
				minY = createdTiles [i].y;
			}
			if (createdTiles [i].y > maxY) 
			{
				maxY = createdTiles [i].y;
			}

			if (createdTiles [i].x < minX) 
			{
				minX = createdTiles [i].x;
			}
			if (createdTiles [i].x > maxX) 
			{
				maxX = createdTiles [i].x;
			}

			xAmount = ((maxX - minX) / tileSize) + extraWallX;
			yAmount = ((maxY - minY) / tileSize) + extraWallY;
		}
	}

	void CreateWalls()
	{
		for (int x = 0; x < xAmount; x++) 
		{
			for (int y = 0; y < yAmount; y++) 
			{
				//instantiates a wall on the position, provided it is unoccupied by a tile. It then adds on infinitely.
				if (!createdTiles.Contains (new Vector3 ((minX - (extraWallX * tileSize) / 2) + (x * tileSize), (minY - (extraWallY * tileSize) / 2) + (y * tileSize)))) 
				{
					Instantiate (wall, new Vector3 ((minX - (extraWallX * tileSize) / 2) + (x * tileSize), (minY - (extraWallY * tileSize) / 2) + (y * tileSize)), transform.rotation);
				}
			}
		}
	}

	public void EnemyDied(Vector3 position)
	{
		enemyAmount--;

		enemyCount.text = "Enemies Remaining: " + enemyAmount.ToString ();

		PlayerScored ();

		if (enemyAmount == 0) {
			print ("Spawn portal at: " + position);
			Instantiate (portal, position, Quaternion.identity);
		}

	}

	public void PlayerScored()
	{
//		if (enemyAmount--) 
//		{
		//adds a point for every enemy killed
			score++; 
			scoreText.text = "Score: " + score.ToString ();
		//gets- and stores- the player's high score
			int hs = GetHighScore ();
			if (score > hs) 
			{
				PlayerPrefs.SetInt ("HighScore", score);
			}
//		}
	}

//	public void EnemiesRemaining()
//	{
//		enemyCount = 10;
//
//	}

	public void PlayerDied()
	{
		//if (playerDead == true)
			gameOver = true; 

	
		gameOverText.SetActive (true);

		//makes the cursor visible if it's Game Over
		if (gameOver = true) 
		{
			Cursor.visible = true;

		}

	}


}
