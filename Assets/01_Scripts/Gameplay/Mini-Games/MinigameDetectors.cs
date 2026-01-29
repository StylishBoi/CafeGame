using UnityEngine.SceneManagement;
using UnityEngine;

public class MinigameDetector : MonoBehaviour
{
    [Header("Visualisers")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private bool maintenanceItem;
    private bool playerInRange;
    
    [Header("Managers")]
    [SerializeField] private CafeUIManager cafeUIManager;
    
    [Header("Minigame")]
    [SerializeField] private GameObject minigamePrefab;
    [SerializeField] private bool deleteableMinigame;
    
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && InventoryManager.Instance.slotsInUsage!=3)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed() && (GameManager.Instance.State==GameState.CafePlay || GameManager.Instance.State==GameState.BasicPlay))
            {
                minigamePrefab.SetActive(true);
                if (deleteableMinigame)
                {
                    MinigameManager.Instance.EnterMinigame(gameObject);
                }
                else
                {
                    MinigameManager.Instance.EnterMinigame();
                }
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
