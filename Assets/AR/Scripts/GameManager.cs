using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Playing,
    GameOver,
    Win
}

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
    private TextMeshProUGUI livesLeftText;

    private bool livesLostScreenActive = false; // Track if the live lost screen is active

    // Singleton instance
    public static GameManager Instance { get; private set; }

    // Game state
    public GameState CurrentState { get; private set; } = GameState.MainMenu;

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

    void Start()
    {
        // Set initial state
        if (SceneManager.GetActiveScene().name == "MainMenu")
            SetState(GameState.MainMenu);
        else if (SceneManager.GetActiveScene().name == "GameScene")
            SetState(GameState.Playing);
        else if (SceneManager.GetActiveScene().name == "GameOver")
            SetState(GameState.GameOver);
    }

    void Update()
    {
        if (CurrentState == GameState.Playing && timerRunning)
            timeSurvived += Time.deltaTime; // Increment time survived
    }

    public void SetState(GameState newState)
    {
        CurrentState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                timerRunning = false;
                break;
            case GameState.Playing:
                timerRunning = true;
                // Ensure liveLostScreen is hidden and flag reset when gameplay starts
                if (liveLostScreen != null) liveLostScreen.SetActive(false);
                livesLostScreenActive = false;
                break;
            case GameState.GameOver:
                timerRunning = false;
                break;
            case GameState.Win:
                timerRunning = false;
                break;
        }
        // Optionally, trigger UI updates or events here
    }

    public void StartGame()
    {
        ResetGame();
        SceneManager.LoadScene("GameScene");
        SetState(GameState.Playing);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        StartCoroutine(DelayedSetMainMenuState());
    }

    private System.Collections.IEnumerator DelayedSetMainMenuState()
    {
        // Wait for the scene to load
        yield return null;
        SetState(GameState.MainMenu);
        Debug.Log("Returned to MainMenu and state set.");
    }

    public void died()
    {
        lives -= 1; // Decrease lives by 1
        if (lives <= 0)
        {
            // Handle game over logic here
            Debug.Log("Game Over");
            score += (timeSurvived * .5f); // Add score based on time survived
            SetState(GameState.GameOver);
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

    public void WinGame()
    {
        SetState(GameState.Win);
        SceneManager.LoadScene("GameOver"); // Or a dedicated Win scene if you have one
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
        // Hide liveLostScreen and reset flag
        if (liveLostScreen != null) liveLostScreen.SetActive(false);
        livesLostScreenActive = false;
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
}
