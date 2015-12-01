using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour 
{
	public GameObject EnemyGO;
	public GameObject AsteroidGO;

	float maxSpawnRateInSeconds = 5f;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Function to spawn an enemy
	void SpawnEnemy()
	{
		// This is the bottom-left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0, 0));

		// This is the top-right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2 (1, 1));

		// INstantiate an enemy
		GameObject anEnemy = (GameObject)Instantiate(EnemyGO);
		anEnemy.transform.position = new Vector2(Random.Range (min.x, max.x), max.y);

		// Schedule when to spaen next enemy
		ScheduleNextEnemySpawn();
	}

	// Function to spawn an asteroid
	void SpawnAsteroid()
	{
		// This is the bottom-left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0, 0));
		
		// This is the top-right point of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2 (1, 1));
		
		// INstantiate an enemy
		GameObject anAsteroid = (GameObject)Instantiate(AsteroidGO);
		anAsteroid.transform.position = new Vector2(Random.Range (min.x, max.x), max.y);

	}

	void ScheduleNextEnemySpawn()
	{
		float spawnInNSeconds;
		if (maxSpawnRateInSeconds > 1f)
		{
			// Pixk a number between 1 and maxSpawnRateInSeconds
			spawnInNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
		}
		else {
			spawnInNSeconds = 1f;
		}

		Invoke("SpawnEnemy", spawnInNSeconds);
		Invoke("SpawnAsteroid", spawnInNSeconds);
	}

	// Function to increase the dificulty of the game
	void IncreaseSpawnRate()
	{
		if (maxSpawnRateInSeconds > 1f)
			maxSpawnRateInSeconds--;

		if (maxSpawnRateInSeconds == 1f) {}
			CancelInvoke("IncreaseSpawnRate");
	}

	// Funcion to start enemy spawner
	public void ScheduleEnemySpawner()
	{
		// Reset max spawn rate
		maxSpawnRateInSeconds = 5f;

		Invoke("SpawnEnemy", maxSpawnRateInSeconds);
		
		// INcrease spawn every 30 seconds
		InvokeRepeating("IncreaseSpawnRate", 0, 30f);
	}


	// Function to stop enemy spawner
	public void UnscheduleEnemySpawer()
	{
		CancelInvoke("SpawnEnemy");
		CancelInvoke("SpawnAsteroid");
		CancelInvoke("IncreaseSpawnRate");
	}
}
