using UnityEngine;
using UnityEngine.AI;

public class Bee : MonoBehaviour
{
    private Player player;

    public float speed = .05f; // Speed of the bee

	private GameManager gameManager;

	//[SerializeField]
	//private SkinnedMeshRenderer beeRenderer; // Renderer for the bee mesh, if applicable
	//private Material beeMaterial; // Material for the bee

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		player = Object.FindFirstObjectByType<Player>();
		gameManager = Object.FindFirstObjectByType<GameManager>();
		//if(beeRenderer) beeMaterial = beeRenderer.material; // Get the material from the renderer
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

		if (player.GetBlueberryFrenzy())
		{
			transform.position -= directionToPlayer * speed/2 * Time.deltaTime;
			Vector3 awayDirection = transform.position - player.transform.position;
			Quaternion awayRotation = Quaternion.LookRotation(awayDirection);
			transform.rotation = awayRotation;
			//if(beeMaterial) beeMaterial.color = new Color { a = 0.5f, r = beeMaterial.color.r, g = beeMaterial.color.g, b = beeMaterial.color.b }; // Change color to indicate frenzy state
		} 
		else
		{
			transform.position += directionToPlayer * speed * Time.deltaTime;
			transform.LookAt(player.transform);
			//if(beeMaterial) beeMaterial.color = new Color { a = 1f, r = beeMaterial.color.r, g = beeMaterial.color.g, b = beeMaterial.color.b }; // Change color to indicate frenzy state
		}
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
