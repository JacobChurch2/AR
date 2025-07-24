using UnityEngine;
using UnityEngine.AI;

public class Bee : MonoBehaviour
{
    private Player player;

    public float speed = .05f; // Speed of the bee

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		player = Object.FindFirstObjectByType<Player>();
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
			Destroy(gameObject);
		}
	}
}
