using UnityEngine;
using UnityEngine.SceneManagement;

public class CoffeeMachineDetector : MonoBehaviour
{
    [Header("Visualisers")]
    [SerializeField] private GameObject visualCue;
    private bool playerInRange;
    
    [Header("Managers")]
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private MinigameManager _minigameManager;
    
    [Header("Result Items")]
    [SerializeField] private Item _goodItem;
    [SerializeField] private Item _badItem;
    
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
            if (InputManager.GetInstance().GetInteractPressed())
            {
                MinigameManager.MiniGameStart(_badItem, _goodItem);
                Debug.Log("Minigame started");
                SceneManager.LoadScene("PouringCafe", LoadSceneMode.Additive);
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

