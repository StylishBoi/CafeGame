using UnityEngine;

public class Dishwasher : MonoBehaviour
{
    int _platesInMachine = 0;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plate"))
        {
            _platesInMachine++;
            
            if (_platesInMachine >= 3)
            {
                MinigameManager.Instance.MiniGameEnd();
            }
        }
    }
}
