using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour 
{
	public GameObject GameManagerGO; // Reference to our game manager

	public GameObject PlayerBulletGO; // this is our player's bullet prefab
	public GameObject bulletPosition01;
	public GameObject bulletPosition02;
	public GameObject ExplosionGO; // This is our explosion prefab

	// Reference to the lives ui text
	public Text LivesUIText;

	const int MaxLives = 3; // maximum player lives
	int lives; // current player lives

	public float speed;

	float accelStartY; // To get the accelerometer y value at the start of the game

	public void Init()
	{
		lives = MaxLives;

		// Update the lives UI text
		LivesUIText.text = lives.ToString();

		// Reset the player position to the center
		transform.position = new Vector2 (0, 0);

		// Set this player game object to active
		gameObject.SetActive(true);
	}

	// Use this for initialization
	void Start () 
	{
		// Get the initial accelerometer y value
		accelStartY = Input.acceleration.y;
	}
	
	// Update is called once per frame
	void Update () 
	{

		// Use for PC
		// The value will be -1, 0 or 1 (for left, no input, and right)
		//float x = Input.GetAxisRaw("Horizontal");
		// The value will be -1, 0 or 1 (for down, no input and up)
		//float y = Input.GetAxisRaw("Vertical");

		// now based on the input we compute a direction vector, and we normalize it to get a unit vector
		//Vector2 direction = new Vector2(x, y).normalized;



		// Use for Mobile
		// Get input from the accelerometer...
		float x = Input.acceleration.x;
		float y = Input.acceleration.y - accelStartY;

		// Create a vector with the accelerometer input valus
		Vector2 direction = new Vector2(x, y);

		// Clamp the length of the vector to a maximun of 1...
		if (direction.sqrMagnitude > 1)
			direction.Normalize();

		// now we call the function that computes and sets the player's position 
		Move(direction);
	}

	// Function to make the player shoot
	public void Shoot()
	{
		// PLay the laser sound effect
		GetComponent<AudioSource>().Play();
		
		// instantiate the first bullet
		GameObject bullet01 = (GameObject)Instantiate (PlayerBulletGO);
		bullet01.transform.position = bulletPosition01.transform.position; // set the bullet initialposition
		
		// instantiate the seconde bullet
		GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
		bullet02.transform.position = bulletPosition02.transform.position; // set the bullet initial position
	}

	void Move(Vector2 direction)
	{
		// Find the screen limits to the player's movement (left, right, top, and bottom edges of the screen).
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2(0, 0)); // this is the bottom-left point (corner) of the screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2(1, 1)); // this is the top-right point (corner  of the screen)

		max.x = max.x - 0.225f; // subtract the player sprite half width
		min.x = min.x + 0.225f; // add the player sprite half width

		max.y = max.y - 0.285f; // subtract the player sprite half height
		min.y = min.y + 0.285f; // add the player sprite half height

		// Get the player's current position
		Vector2 pos = transform.position;

		// Calculate the new position
		pos += direction * speed * Time.deltaTime;

		// Make sure the new position is not outside the screen
		pos.x = Mathf.Clamp(pos.x, min.x, max.x);
		pos.y = Mathf.Clamp(pos.y, min.y, max.y);

		// Update the player's
		transform.position = pos;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// Detect collision of the the player ship with an enemy ship, or with an enemy bullet
		if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag") || (col.tag == "AsteroidTag")) 
		{
			PlayExplosion();
			lives--; // subtract one live
			LivesUIText.text = lives.ToString(); // Update Lives UI text

			// If our player is dead
			if (lives == 0) 
			{
				// Change game manager state to game over state
				GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

				// hide the player's ship
				gameObject.SetActive(false);
			}
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





