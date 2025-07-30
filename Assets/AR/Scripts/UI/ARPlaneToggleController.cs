using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ARPlaneToggleController : MonoBehaviour
{
    [SerializeField] private Toggle planeToggle;
    [SerializeField] private ARPlaneManager planeManager;
    
    private bool planeVisualizationEnabled = true;

    void Start()
    {
        // Find the ARPlaneManager if not assigned
        if (planeManager == null)
            planeManager = FindFirstObjectByType<ARPlaneManager>();
            
        // Set up the toggle
        if (planeToggle != null)
        {
            planeToggle.isOn = planeVisualizationEnabled;
            planeToggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
    }

    public void OnToggleValueChanged(bool isOn)
    {
        planeVisualizationEnabled = isOn;
        UpdatePlaneVisualization();
    }

    private void UpdatePlaneVisualization()
    {
        if (planeManager == null) return;

        // Update all existing planes
        foreach (var plane in planeManager.trackables)
        {
            // Get the MeshRenderer component to control visibility
            MeshRenderer meshRenderer = plane.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = planeVisualizationEnabled;
            }
            
            // Also try to get LineRenderer for the plane outline
            LineRenderer lineRenderer = plane.GetComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.enabled = planeVisualizationEnabled;
            }
        }
    }

    // Public method to be called from UI
    public void TogglePlaneVisualization()
    {
        planeVisualizationEnabled = !planeVisualizationEnabled;
        if (planeToggle != null)
            planeToggle.isOn = planeVisualizationEnabled;
        UpdatePlaneVisualization();
    }

    // Method to set plane visualization state
    public void SetPlaneVisualization(bool enabled)
    {
        planeVisualizationEnabled = enabled;
        if (planeToggle != null)
            planeToggle.isOn = enabled;
        UpdatePlaneVisualization();
    }
} 