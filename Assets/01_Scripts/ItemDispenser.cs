using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemDispenser : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    
    public ItemPickUp itemPickUp;
    
    private bool playerInRange;
    
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                itemPickUp.PickUpItem();
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}

