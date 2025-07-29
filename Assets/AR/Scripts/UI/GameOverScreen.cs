using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
	GameManager gameManager;

	[SerializeField]
	TextMeshProUGUI scoreText;

	public void ReturnToMainMenu()
	{
		gameManager.ResetGame(); // Reset the game state
		SceneManager.LoadScene("MainMenu");
	}

	private void Start()
	{
		gameManager = GameManager.Instance; // Get the GameManager instance
		if (gameManager)
		{
			scoreText.text = $"Score: {gameManager.score:F2}\n" +
				$"Enemies Killed: {gameManager.enemiesKilled}\n" +
				$"Time Survived: {gameManager.timeSurvived:F2} seconds";
		}
		else
		{
			scoreText.text = "How did you do this!";
		}
	}
}
