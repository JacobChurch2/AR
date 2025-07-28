using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerupEffect : MonoBehaviour
{
    public Image overlayImage;     // Drag the UI Image in the Inspector
    private float overlayDuration = 10f;
    public Color powerupColor = new Color(0.2f, 0.6f, 1f, 0.3f); // Custom color with transparency

    private Coroutine overlayRoutine;

    public void ActivatePowerup()
    {
        if (overlayRoutine != null) StopCoroutine(overlayRoutine);

        overlayRoutine = StartCoroutine(ShowOverlay());
    }

    private IEnumerator ShowOverlay()
    {
        overlayImage.color = powerupColor;
        overlayImage.enabled = true;

        yield return new WaitForSeconds(overlayDuration);

        overlayImage.enabled = false;
        overlayRoutine = null;
    }
}
