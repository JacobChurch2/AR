using UnityEngine;

public class Berry : MonoBehaviour
{
    public enum BerryType
	{
		Strawberry,
		Apple,
        Grape,
		Blueberry,
	}

	[SerializeField]
	public BerryType berryType;
	private GameManager gameManager;

	private void Start()
	{
		gameManager = Object.FindFirstObjectByType<GameManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Player>())
		{
			// Handle interaction with the player
			gameManager.AddScore(GetPoints()); // Add points to the score
			Debug.Log($"Player collected a {berryType}!");
			if(berryType == BerryType.Blueberry)
			{
				// If the berry is a blueberry, activate blueberry frenzy
				other.GetComponent<Player>().startBlueberryFrenzy();
			}
			Destroy(gameObject); // Destroy the berry after collection
		}
	}

	public float GetPoints()
	{
		// This method can be expanded to handle scoring logic
		switch (berryType)
		{
			case BerryType.Strawberry:
				return 1f;
			case BerryType.Apple:
				return 5f;
			case BerryType.Grape:
				return 10f;
			case BerryType.Blueberry:
				return 20f;
			default:
				return 0f;
		}
	}
}
