using UnityEngine;
using UnityEngine.Android;

public class Pickup : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		//TODO: implement player check
		OnPickup();
	}

	// This method is called when the object is picked up
	//TODO: add playerInteraction
	public void OnPickup()
	{
		

		// Optionally, you can destroy the object after picking it up
		Destroy(gameObject);
	}
}
