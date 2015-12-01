using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour 
{
	Text timeUI; // Reference to the time counter UI Text
	float startTime; // The time when the user clicks on play
	float ellapsedTime; // The ellapsed time after the user clicks on play
	bool startCounter; // Flag to start the counter

	int minutes;
	int seconds;

	// Use this for initialization
	void Start () 
	{
		startCounter = false;

		// Get the Text UI component from this gameObject
		timeUI = GetComponent<Text>();
	}

	// Function to start the time counter
	public void StartTimeCounter()
	{
		startTime = Time.time;
		startCounter = true;
	}

	// function to stop the time counter
	public void StopTimeCounter()
	{
		startCounter = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startCounter)
		{
			// Compute the ellapsed time
			ellapsedTime = Time.time - startTime;

			minutes = (int)ellapsedTime / 60; // get the minutes
			seconds = (int)ellapsedTime % 60; // get the seconds

			// Update the time counter UI Text
			timeUI.text = string.Format("{0:00}:{1:00}", minutes, seconds);
		}
	}
}
