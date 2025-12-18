using UnityEngine;

public class Dishwasher : MonoBehaviour
{
    int platesInMachine = 0;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plate"))
        {
            platesInMachine++;
            
            if (platesInMachine >= 3)
            {
                MinigameManager.Instance.MiniGameEnd();
            }
        }
    }
}
