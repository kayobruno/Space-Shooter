using UnityEngine;
using System.Collections;

public class StarGenerator : MonoBehaviour 
{
	public GameObject StarGO; // this is our StarGO prefab
	public int MaxStars;

	// Array of colors
	Color[] starColors = {
		new Color (0.5f, 0.5f, 1f), // Blue
		new Color (0, 1f, 1f), // Green
		new Color (1f, 1f, 0), // Yellow
		new Color (1f, 0, 0), // Red
	};

	// Use this for initialization
	void Start () 
	{
		// This is the bottom-left point ofthe screen
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2(0, 0));

		// This is the top-right point ofthe screen
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2(1, 1));

		// Loop to create the stars
		for (int i = 0; i < MaxStars; ++i)
		{
			GameObject star = (GameObject)Instantiate(StarGO);

			// Set the star color
			star.GetComponent<SpriteRenderer>().color = starColors[i % starColors.Length];

			// Set the position of the star (random x and random y)
			star.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

			// Set a random speed for the star
			star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

			// make the star a child of the StarGeneratorGO
			star.transform.parent = transform;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
