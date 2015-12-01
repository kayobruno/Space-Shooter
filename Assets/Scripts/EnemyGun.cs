using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour 
{
	public GameObject EnemyBulletGO; //This is our enemy bullet prefab

	// Use this for initialization
	void Start () 
	{
		// Fire an enemy bullet after 1 second
		Invoke("FireEnemyBullet", 1f);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Function to fire an enenmy bullet
	void FireEnemyBullet()
	{
		// Get a reference to the player's ship
		GameObject playerShip = GameObject.Find("PlayerGO");

		if (playerShip != null) // if the player is not dead
		{
			// Instantiate an enemy bullet
			GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);

			// Set the bullet's initial position
			bullet.transform.position = transform.position;

			// compute the bullet's direction towards the player's ship
			Vector2 direction = playerShip.transform.position - bullet.transform.position;

			// Set the bullet's direction
			bullet.GetComponent<EnemyBullet>().SetDirection(direction);
		}
	}
}
