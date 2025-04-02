using UnityEngine;
using UnityEngine.SceneManagement;

public class CoffeeIncrease : MonoBehaviour
{
    private bool pipeStopped;
    private bool success;

    void FixedUpdate()
    {
        if (pipeStopped != true)
        {
            this.transform.localScale += new Vector3(0f, 1f, 0f)*Time.deltaTime;
            this.transform.localPosition += new Vector3(0f, 0.5f, 0f)*Time.deltaTime;
        }
        
        if (MinigameInput.GetInstance().GetInteractPressed() && pipeStopped == false)
        {
            Debug.Log(MinigameInput.GetInstance().GetInteractPressed());
            pipeStopped = true;
            
            if (success)
            {
                Debug.Log("Amazing");
                MinigameManager.MiniGameEnd();
            }
            else
            {
                Debug.Log("Fine");
                MinigameManager.MiniGameEnd();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Occured");
        
        if (collision.CompareTag("EndZone"))
        {
            pipeStopped = true;
            Debug.Log("Terrible !");
            MinigameManager.MiniGameEnd();
        }
        
        else if (collision.CompareTag("SuccessZone"))
        {
            success = true;
        }
    }
    
}
