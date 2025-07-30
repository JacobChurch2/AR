using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ScanProcess : MonoBehaviour
{
    [SerializeField]
	private GameManager gameManager;

	[SerializeField]
	private ARPlaceObject arPlaceObject;

	[SerializeField]
	private GameObject scanProcessUI;

	[SerializeField]
	private ARPlaneManager planeManager;

	[SerializeField]
	private GameObject scanCompleteUI;
		
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		gameManager ??= GetComponent<GameManager>();
		if (gameManager) gameManager.enabled = false; // Disable GameManager script at the start of the scan process

		arPlaceObject ??= FindFirstObjectByType<ARPlaceObject>(); // Get the ARPlaceObject instance
		if (arPlaceObject) arPlaceObject.enabled = false; // Disable ARPlaceObject script during the scan process

		planeManager ??= FindFirstObjectByType<ARPlaneManager>();

		scanProcessUI.SetActive(true); // Activate the scan process UI
	}

	private void Update()
	{
		float totalPlaneArea = 0f; // Initialize total plane area
		foreach (var plane in planeManager.trackables)
		{
			if (plane.alignment == PlaneAlignment.HorizontalUp)
			{
				totalPlaneArea += plane.size.x * plane.size.y;
			}
		}
		if (totalPlaneArea >= 5)
		{
			scanCompleteUI.SetActive(true); // Show scan complete UI
		}
		else
		{
			scanCompleteUI.SetActive(false); // Hide scan complete UI if no planes are detected
		}
	}

	public void StartGameAfterScanning()
	{
		if (gameManager)
		{
			gameManager.enabled = true; // Enable GameManager script after scanning
			arPlaceObject.enabled = true; // Enable ARPlaceObject script to start placing objects
			scanProcessUI.SetActive(false); // Deactivate the scan process UI
			this.enabled = false; // Disable this script after starting the game
		}
		else
		{
			Debug.LogError("GameManager is not assigned or found in the scene.");
		}
	}

	
}
