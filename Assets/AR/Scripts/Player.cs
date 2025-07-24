using UnityEngine;

public class Player : MonoBehaviour
{
    float points = 0f; // Points earned by the player

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupBerry(Berry berry)
    {
		AddPoints(berry.GetPoints());
	}

    private void AddPoints(float amount)
	{
		points += amount;
		//Debug.Log("Points added: " + amount + ". Total points: " + points);
	}
}
