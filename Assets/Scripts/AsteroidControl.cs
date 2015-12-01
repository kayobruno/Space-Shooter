using UnityEngine;
using System.Collections;

public class AsteroidControl : MonoBehaviour 
{
	public GameObject ExplosionGO; // this is our explosion prefab

	GameObject scoreUITextGO;

	float speed;

	// Use this for initialization
	void Start () 
	{
		speed = 2.5f;

		// Get the score UI
		scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Get the asteroid current position
		Vector2 position = transform.position;

		// Compute the enemy new position
		position = new Vector2 (position.x, position.y - speed * Time.deltaTime);
		
		// Update the enemy position
		transform.position = position;
		
		// This is the bottom-left point of the screen
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		
		// If the enemy went outside the screen on the bottom, the destroy the enemy
		if (transform.position.y < min.y)
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of the enemy ship with the player ship, or with a player's bullet
		if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag"))
		{
			PlayExplosion();

			// add 100 points to the score
			scoreUITextGO.GetComponent<GameScore>().Score += 50;

			// Destroy this enemy ship
			Destroy (gameObject);
		}

		if (col.tag == "EnemyBulletTag") 
		{
			PlayExplosion();
			Destroy (gameObject);
		}
	}
	
	// Function to instantiate an explosion
	void PlayExplosion()
	{
		GameObject explosion = (GameObject) Instantiate(ExplosionGO);
		
		// Set the position of the explosion
		explosion.transform.position = transform.position;
	}
}
