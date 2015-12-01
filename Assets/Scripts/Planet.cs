using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour 
{
	public float speed; // The speed of the planet
	public bool isMoving; // Flag to make the planet scroll down the screen

	Vector2 min; // this is the botto-left point of the screen
	Vector2 max; // this is the top-right point of the screen

	void Awake()
	{
		isMoving = false;

		min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		// Add the planet sprite half height to max y
		max.y = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;

		// Subtract the planet sprite half height to min y
		min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isMoving)
			return;

		// Get the current position of the planet
		Vector2 position = transform.position;

		// Compute the planet's new position
		position = new Vector2 (position.x, position.y + speed * Time.deltaTime);

		// Update the planet's position
		transform.position = position;

		// If the planet gets to the minimum y position. Then stop moving the planet
		if (transform.position.y < min.y)
		{
			isMoving = false;
		}
	}

	// Function to reset the planet's position
	public void ResetPosition()
	{
		// Reset the position of the planet to random x, and max y
		transform.position = new Vector2(Random.Range (min.x, max.x), max.y);
	}
}
