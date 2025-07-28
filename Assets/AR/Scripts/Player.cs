using UnityEngine;

public class Player : MonoBehaviour
{
    private bool blueberryFrenzy = false;
	private float blueberryFrenzyDuration = 10f; // Duration of the blueberry frenzy in seconds
    private float blueberryFrenzyTimer = 0f; // Timer for the blueberry frenzy

    // Update is called once per frame
    void Update()
    {
        if(blueberryFrenzy)
		{
			blueberryFrenzyTimer -= Time.deltaTime;
			if (blueberryFrenzyTimer <= 0f)
			{
				blueberryFrenzy = false; // End the frenzy when the timer runs out
			}
		}
	}

    public void startBlueberryFrenzy()
    {
		blueberryFrenzy = true;
		blueberryFrenzyTimer = blueberryFrenzyDuration;
	}

    public bool GetBlueberryFrenzy()
	{
		return blueberryFrenzy;
	}   
}
