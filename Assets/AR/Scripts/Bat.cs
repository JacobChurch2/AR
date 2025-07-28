using UnityEngine;

public class bat : MonoBehaviour
{
	private Player player;
    private float speed = 1f; // Speed of the bat
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
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
		if (!GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider>().bounds))
		{
			Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
			transform.position += directionToPlayer * speed * Time.deltaTime;
			transform.LookAt(player.transform);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Player>())
		{
			if (player.GetBlueberryFrenzy())
			{
				// Player is in blueberry frenzy, bee is destroyed
				gameManager.AddScore(50f); // Add points for destroying the bee
				gameManager.enemiesKilled += 1; // Increment the count of enemies killed
				Destroy(gameObject);
				return;
			}
			gameManager.died(); // Decrease lives by 1
			Destroy(gameObject);
		}
	}
}
