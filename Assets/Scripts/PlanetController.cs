using UnityEngine;
using System.Collections;
using System.Collections.Generic; //For Queue

public class PlanetController : MonoBehaviour 
{
	public GameObject[] Planets; // an array of PlanetGO prefab

	// Queue to hold the plants
	Queue<GameObject> availablePlanets = new Queue<GameObject>();

	// Use this for initialization
	void Start () 
	{
		// add the planets to the Queue (Enqueue them)
		availablePlanets.Enqueue (Planets[0]);
		availablePlanets.Enqueue (Planets[1]);
		availablePlanets.Enqueue (Planets[2]);
		availablePlanets.Enqueue (Planets[3]);
		availablePlanets.Enqueue (Planets[4]);
		availablePlanets.Enqueue (Planets[5]);

		// call the MovePLanetDown function
		InvokeRepeating("MovePlanetDown", 0, 20f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	// Function to desqueue a planet, and set its isMoving flag to true
	// so that the planet starts scrolling down the screen
	void MovePlanetDown()
	{
		EnqueuePlants();

		// If the Queue is empty, then return
		if (availablePlanets.Count == 0)
			return;

		// Get a planet from the queue
		GameObject aPlanet = availablePlanets.Dequeue();

		// Set the planet isMoving flag to true
		aPlanet.GetComponent<Planet>().isMoving = true;
	}

	// Function to Enqueue planets that are below the screen and are not moving
	void EnqueuePlants()
	{
		foreach (GameObject aPlanet in Planets)
		{
			// if the planet is below the screen, and the planet is not moving
			if((aPlanet.transform.position.y < 0) && (!aPlanet.GetComponent<Planet>().isMoving))
			{
				// Reset the planet position
				aPlanet.GetComponent<Planet>().ResetPosition();

				// Enqueue the planet
				availablePlanets.Enqueue(aPlanet);
			}
		}
	}
}
