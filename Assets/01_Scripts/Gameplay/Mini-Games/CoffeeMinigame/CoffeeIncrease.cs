using UnityEngine;
using UnityEngine.SceneManagement;

public class CoffeeIncrease : MonoBehaviour
{
    [Header("Minigames Awards")]
    [SerializeField] Item goodMinigameItem;
    [SerializeField] Item badMinigameItem;
    
    private bool pipeStopped;
    private bool success;

    void FixedUpdate()
    {
        if (pipeStopped != true)
        {
            this.transform.localScale += new Vector3(0f, 0.4f, 0f)*Time.deltaTime;
            this.transform.localPosition += new Vector3(0f, 0.2f, 0f)*Time.deltaTime;
        }
        
        if (MinigameInput.Instance.GetInteractPressed() && pipeStopped == false)
        {
            Debug.Log(MinigameInput.Instance.GetInteractPressed());
            pipeStopped = true;
            
            if (success)
            {
                InventoryManager.Instance.AddItem(goodMinigameItem);
                MinigameManager.MiniGameEnd();
            }
            else
            {
                InventoryManager.Instance.AddItem(badMinigameItem);
                MinigameManager.MiniGameEnd();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Occured");
        
        if (collision.CompareTag("EndZone"))
        {
            Debug.Log("Not a success");
            pipeStopped = true;
            InventoryManager.Instance.AddItem(badMinigameItem);
            MinigameManager.MiniGameEnd();
        }
        
        else if (collision.CompareTag("SuccessZone"))
        {
            Debug.Log("Currently successful");
            success = true;
        }
    }
    
}
