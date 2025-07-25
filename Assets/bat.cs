using UnityEngine;

public class bat : MonoBehaviour
{
	private Player player;
    private float speed = 1f; // Speed of the bat
								 // Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        player = Object.FindFirstObjectByType<Player>();
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
			Destroy(gameObject);
		}
	}
}
