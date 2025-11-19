using UnityEngine;

public class Dishwasher : MonoBehaviour
{
    int platesInMachine = 0;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        
        if (collision.CompareTag("Plate"))
        {
            Debug.Log("A plate has been added to the dishwasher!");
            platesInMachine++;
            
            if (platesInMachine >= 3)
            {
                Debug.Log("Dishwasher is full!");
                MinigameManager.MiniGameEnd();
            }
        }
    }
}
