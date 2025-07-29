using UnityEngine;

public class Bat : MonoBehaviour
{
	private Player player;
    public float speed = 1f; // Speed of the bat
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

		if (player.GetBlueberryFrenzy())
		{
			transform.position -= directionToPlayer * speed / 2 * Time.deltaTime;
			Vector3 awayDirection = transform.position - player.transform.position;
			Quaternion awayRotation = Quaternion.LookRotation(awayDirection);
			transform.rotation = awayRotation;
			//if(beeMaterial) beeMaterial.color = new Color { a = 0.5f, r = beeMaterial.color.r, g = beeMaterial.color.g, b = beeMaterial.color.b }; // Change color to indicate frenzy state
		}
		else
		{
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
			if (!GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider>().bounds))
			{
				transform.position += directionToPlayer * speed * Time.deltaTime;
				transform.LookAt(player.transform);
			}
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
