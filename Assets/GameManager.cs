using UnityEngine;

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
        //TODO: Implement game over logic here
    }

	public void died()
	{
		lives -= 1; // Decrease lives by 1
		if (lives <= 0)
		{
			// Handle game over logic here, e.g., show game over screen, reset game, etc.
			Debug.Log("Game Over");
			// Optionally, you can reset the game or load a game over scene
			// SceneManager.LoadScene("GameOverScene");
		}
		else
		{
			Debug.Log($"Lives remaining: {lives}");
		}
	}

	public void AddScore(float points)
	{
		score += points; // Add points to the score
		Debug.Log($"Score: {score}");
	}
}
