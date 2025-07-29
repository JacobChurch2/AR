using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathUILogic : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI livesLeftText;

    [SerializeField]
    Button restartButton;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        Object.FindFirstObjectByType<GameManager>().liveLostScreen = this.gameObject; // Reset the game state
		if (livesLeftText)
		{
			Object.FindFirstObjectByType<GameManager>().livesLeftText = livesLeftText; // Set the lives left text reference
		}

		if (restartButton) restartButton.onClick.AddListener(() => Object.FindFirstObjectByType<GameManager>().playAgainAfterLostALife()); // Add listener to restart button

		gameObject.SetActive(false); // Disable this script until needed
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
