using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	// Reference to our game objects
	public GameObject playButton;
	public GameObject playerShip;
	public GameObject enemySpawner; // Reference to our enemy spawner
	public GameObject GameOverGO; // Reference to the game over image
	public GameObject scoreUITextGO; // Reference to the score text UI game object
	public GameObject TimeCounterGO; // Reference to the time counter game object
	public GameObject GameTitleGO; // Reference to the GameTitleGO
	public GameObject ShootButton; // Reference to the shoot button game object
	public GameObject ButtonExitGame; // Reference to the button exit Game
	public GameObject GameDeveloperTitle; // Reference the title developer


	public enum GameManagerState
	{
		Opening,
		GamePlay,
		GameOver
	}

	GameManagerState GMState;

	// Use this for initialization
	void Start () 
	{
		GMState = GameManagerState.Opening;
	}
	
	// Function to update the game manager state
	void UpdateGameManagerState()
	{
		switch (GMState) 
		{
			case GameManagerState.Opening:

				// Hide game over
				GameOverGO.SetActive(false);

				// Display the game title
				GameTitleGO.SetActive(true);

				// Display developer
				GameDeveloperTitle.SetActive(true);

				// Set play button visible (active)
				playButton.SetActive(true);

				// Display button exit game
				ButtonExitGame.SetActive(true);

				break;
			case GameManagerState.GamePlay:

				// Reset the score
				scoreUITextGO.GetComponent<GameScore>().Score = 0;
				
				// Hide play button on game play state
				playButton.SetActive(false);

				// Hide the game title
				GameTitleGO.SetActive(false);

				// Hide Developer
				GameDeveloperTitle.SetActive(false);

				// Display the shoot button
				ShootButton.SetActive(true);

				// Hide button exit game
				ButtonExitGame.SetActive(false);

				// Set the player visible (active) and init the player
				playerShip.GetComponent<PlayerControl>().Init();

				// Start enemy spawner
				enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

				// Start the time counter
				TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

				break;
			case GameManagerState.GameOver:

				// Stop enemy spawner
				TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

				// Stop enemy spawner
				enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawer();

				// Hide the shoot button
				ShootButton.SetActive(false);

				// Display game over
				GameOverGO.SetActive(true);

				// Change game manager state to Opening state after 8 seconds
				Invoke("ChangeToOpeningState", 3f);

				break;
		} 
	}

	// Function to set the game manager state
	public void SetGameManagerState(GameManagerState state)
	{
		GMState = state;
		UpdateGameManagerState();
	}

	// Our player button will call this function
	// When the user clicks the button.
	public void StartGamePlay()
	{
		GMState = GameManagerState.GamePlay;
		UpdateGameManagerState();
	}

	// Function to change game manager state to opening state
	public void ChangeToOpeningState()
	{
		SetGameManagerState(GameManagerState.Opening);
	}
	
}
