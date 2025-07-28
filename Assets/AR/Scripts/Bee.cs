using UnityEngine;
using UnityEngine.AI;

public class Bee : MonoBehaviour
{
    private Player player;

    public float speed = .05f; // Speed of the bee

	private GameManager gameManager;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		player = Object.FindFirstObjectByType<Player>();
		gameManager = Object.FindFirstObjectByType<GameManager>();
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

		transform.position += directionToPlayer * speed * Time.deltaTime;

		transform.LookAt(player.transform);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Player>())
		{
			if (player.GetBlueberryFrenzy())
			{
				// Player is in blueberry frenzy, bee is destroyed
				gameManager.AddScore(25f); // Add points for destroying the bee
				gameManager.enemiesKilled += 1; // Increment the count of enemies killed
				Destroy(gameObject);
				return;
			}
			// Player has been hit by the bee
			gameManager.died(); // Decrease lives by 1
			Destroy(gameObject);
		}
	}
}
