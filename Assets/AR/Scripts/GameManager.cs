using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float enemiesKilled = 0f;
	public float timeSurvived = 0f;
	private float score = 0f;
	private int lives = 3;

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
		timeSurvived += Time.deltaTime; // Increment time survived
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

			foreach (var bat in Object.FindObjectsByType<bat>(FindObjectsSortMode.None))
			{
				Destroy(bat.gameObject); // Destroy all bats when player dies
			}

			foreach (var bee in Object.FindObjectsByType<Bee>(FindObjectsSortMode.None))
			{
				Destroy(bee.gameObject); // Destroy all bees when player dies
			}
		}
	}

	public void AddScore(float points)
	{
		score += points; // Add points to the score
		Debug.Log($"Score: {score}");
	}
}
