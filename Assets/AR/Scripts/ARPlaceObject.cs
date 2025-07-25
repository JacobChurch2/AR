using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceObject : MonoBehaviour
{
	[SerializeField] private ARRaycastManager raycastManager;

	[SerializeField] private GameObject[] prefabs;

	[SerializeField] private ARPlaneManager planeManager;

	private List<ARPlane> planes = new List<ARPlane>();
	bool isPlacing = false;

	[SerializeField] private float minSpawnInterval = 5f; // Time in seconds between spawns
	[SerializeField] private float maxSpawnInterval = 6f; // Time in seconds between spawns
	private float spawnTimer = 0f;

	[SerializeField] private int beeIndex = 0;
	[SerializeField] private int batIndex = 1;
	[SerializeField] private int blueberryIndex = 2;
	[SerializeField] private int fruitMinIndex = 3;
	[SerializeField] private int fruitMaxIndex = 6;

	void Start()
	{
		// Get the ARRaycastManager component if it's not already assigned
		raycastManager ??= GetComponent<ARRaycastManager>();
		planeManager ??= GetComponent<ARPlaneManager>();

		spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
	}

	void Update()
	{
		// Exit early if ARRaycastManager is not assigned
		if (raycastManager == null) return;
		if (planeManager == null) return;

		// Update the spawn timer
		spawnTimer -= Time.deltaTime;

		if (spawnTimer <= 0f)
		{
			SpawnItem();
			spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
		}

		//// Handle touch input (on phones/tablets)
		//if (Touchscreen.current != null &&
		//		   Touchscreen.current.touches.Count > 0 &&
		// Touchscreen.current.touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began &&
		//		   !isPlacing)
		//{
		//	isPlacing = true;

		//	// Get touch position on screen
		//	Vector2 touchPos = Touchscreen.current.touches[0].position.ReadValue();

		//	// Place the object at touch position
		//	PlaceObject(touchPos);
		//}
		//// Handle mouse input (for desktop testing)
		//else if (Mouse.current != null &&
		//					  Mouse.current.leftButton.wasPressedThisFrame &&
		//					  !isPlacing)
		//{
		//	isPlacing = true;

		//	// Get mouse position on screen
		//	Vector2 mousePos = Mouse.current.position.ReadValue();

		//	// Place the object at mouse click
		//	PlaceObject(mousePos);
		//}

		//planeManager.trackablesChanged += OnPlanesChanged;

	}

	private void SpawnItem()
	{
		int planeCount = planes.Count;
		if (planeCount == 0) return;

		int randomIndex = Random.Range(0, planeCount);

		ARPlane randomPlane = planes[randomIndex];
		if (randomPlane == null) return; // Safety check

		Vector3 randomPosition = randomPlane.center + new Vector3(
			Random.Range(-randomPlane.size.x / 2, randomPlane.size.x / 2),
			0.3f, // Slightly above the plane to avoid clipping
			Random.Range(-randomPlane.size.y / 2, randomPlane.size.y / 2));
		Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

		float chance = Random.Range(0, 100);

		if(chance < 40) // 40% chance to spawn a fruit
		{
			int randomFruitIndex = Random.Range(fruitMinIndex, fruitMaxIndex + 1);
			Instantiate(prefabs[randomFruitIndex], randomPosition, randomRotation);
		}
		else if (chance < 63) // 23% chance to spawn a bee
		{
			Instantiate(prefabs[beeIndex], randomPosition, randomRotation);
		}
		else if (chance < 80) // 17% chance to spawn a blueberry
		{
			Instantiate(prefabs[blueberryIndex], randomPosition, randomRotation);
		}
		else if (chance < 92) // 12% chance to spawn a bat
		{
			Instantiate(prefabs[batIndex],randomPosition, randomRotation);
		}
		// 8% chance to spawn nothing
	}

	public void OnPlanesChanged()
	{
		planes.Clear();

		foreach (var plane in planeManager.trackables)
		{
			if (plane.alignment == PlaneAlignment.HorizontalUp)
			{
				planes.Add(plane);
			}
		}
	}


	void PlaceObject(Vector2 position)
	{
		// Create a list to store raycast hit results
		var rayHits = new List<ARRaycastHit>();

		// Raycast from screen position into AR world using  trackable type
		raycastManager.Raycast(position, rayHits, TrackableType.PlaneWithinPolygon);

		// If we hit a valid surface
		if (rayHits.Count > 0)
		{
			// Get the position and rotation of the first hit
			Vector3 hitPosePosition = rayHits[0].pose.position;
			Quaternion hitPoseRotation = rayHits[0].pose.rotation;

			// Instantiate the prefab at the hit location
			Quaternion rotation = hitPoseRotation * Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
			Instantiate(prefabs[Random.Range(0, prefabs.Length)], hitPosePosition, rotation);
		}

		// Wait briefly before allowing another placement
		StartCoroutine(SetPlacingToFalseWithDelay());
	}

	IEnumerator SetPlacingToFalseWithDelay()
	{
		// Wait for a short delay
		yield return new WaitForSeconds(0.25f);

		// Allow placing again
		isPlacing = false;
	}
}
