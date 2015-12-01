using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour 
{
	float speed; // The bullet speed
	Vector2 _direction; // The direction of the bullet
	bool isReady; // To know when the bullet direction is set

	// set default values is Awake function
	void Awake()
	{
		speed = 5f;
		isReady = false;
	}

	// Use this for initialization
	void Start () 
	{
	
	}

	// Function to set the bullet's direction
	public void SetDirection(Vector2 direction)
	{
		// set the direction normalized, to get an unit vector 
		_direction = direction.normalized;

		isReady = true; // set flag to true
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isReady) 
		{
			// get the bullet's current position
			Vector2 position = transform.position;

			// Compute the bullet's new position
			position += _direction * speed * Time.deltaTime;

			// Update the bullet's position
			transform.position = position;

			// Next we need to remove the bullet from our game
			// If the bullet goes outside the screen

			// This is the bottom-left point of the screen
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

			// This is the top-right point of the screen
			Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

			// if the bullet goes outside the screen, them destroy it
			if ((transform.position.x < min.x) || (transform.position.x > max.x) ||
			    (transform.position.y < min.y) || (transform.position.y > max.y))
			{
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of an enemy's bullet with the player ship
		if (col.tag == "PlayerShipTag")
		{
			// Destroy this enemy's bullet
			Destroy (gameObject);
		}
	}
}
