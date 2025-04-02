using UnityEngine;
using System.Collections;

public class NPCTEST : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private Item[] listOfItems;
    [SerializeField] private SpriteRenderer imageRequested;
    
    private int random;

    private Item desiredItem;
    
    public ItemPickUp itemPickUp;
    
    private bool playerInRange;
    
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        
        random = Random.Range(0, listOfItems.Length);
        desiredItem = listOfItems[random];

        imageRequested.sprite=desiredItem.image;
        
        Debug.Log(desiredItem.name);
    }

    void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                itemPickUp.RemoveSelectedItem();
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
