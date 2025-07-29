using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float enemiesKilled = 0f;
	public float timeSurvived = 0f;
	private bool timerRunning = true; // Timer state
	public float score = 0f;
	private int lives = 3;

	[SerializeField]
	private GameObject liveLostScreen;
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
	private TextMeshProUGUI livesLeftText;

	private bool livesLostScreenActive = false; // Track if the live lost screen is active
	private bool pausedScreenActive = false; // Track if the live lost screen is active

	// Singleton instance
	public static GameManager Instance { get; private set; }
	// Awake is called when the script instance is being loaded
	void Awake()
	{
		// Ensure that there is only one instance of GameManager
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject); // Keep this instance across scenes
		}
		else
		{
			Destroy(gameObject); // Destroy duplicate instances
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (timerRunning) timeSurvived += Time.deltaTime; // Increment time survived
	}

	public void died()
	{
		lives -= 1; // Decrease lives by 1
		if (lives <= 0)
		{
			// Handle game over logic here, e.g., show game over screen, reset game, etc.
			Debug.Log("Game Over");

			score += (timeSurvived * .5f); // Add score based on time survived
										 // Optionally, you can reset the game or load a game over scene
			SceneManager.LoadScene("GameOver");
		}
		else
		{
			Debug.Log($"Lives remaining: {lives}");
			
			foreach (var berry in Object.FindObjectsByType<Berry>(FindObjectsSortMode.None))
			{
				Destroy(berry.gameObject); // Destroy all berries when player dies
			}

			foreach (var bat in Object.FindObjectsByType<Bat>(FindObjectsSortMode.None))
			{
				Destroy(bat.gameObject); // Destroy all bats when player dies
			}

			foreach (var bee in Object.FindObjectsByType<Bee>(FindObjectsSortMode.None))
			{
				Destroy(bee.gameObject); // Destroy all bees when player dies
			}

			timerRunning = false; // Stop the timer
			liveLostScreen.SetActive(true); // Show the live lost screen
			livesLeftText.text = $"Lives Remaining: \n{lives}"; // Update the lives left text
			livesLostScreenActive = true; // Set the lives lost screen as active
		}
	}

	public void playAgainAfterLostALife()
	{
		liveLostScreen.SetActive(false); // Hide the live lost screen
		timerRunning = true; // Restart the timer
		livesLostScreenActive = false; // Reset the lives lost screen state
	}

	public void ResetGame()
	{
		lives = 3; // Reset lives to 3
		enemiesKilled = 0f; // Reset enemies killed
		timeSurvived = 0f; // Reset time survived
		score = 0f; // Reset score
		timerRunning = true; // Restart the timer
		

		Debug.Log("Game has been reset.");
		Debug.Log($"Lives remaining: {lives}");
		Debug.Log($"Score: {score}");
		Debug.Log($"Enemies killed: {enemiesKilled}");
		Debug.Log($"Time survived: {timeSurvived}");
	}

	public void AddScore(float points)
	{
		score += points; // Add points to the score
		Debug.Log($"Score: {score}");
	}

	public bool IsLivesLostScreenActive()
	{
		return livesLostScreenActive; // Return the state of the lives lost screen
	}
    public void PauseGame()
    {
        pausedScreenActive = true;
        Time.timeScale = 0f;
        if (pauseScreen != null)
            pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        pausedScreenActive = false;
        Time.timeScale = 1f;
        if (pauseScreen != null)
            pauseScreen.SetActive(false);
    }
}
