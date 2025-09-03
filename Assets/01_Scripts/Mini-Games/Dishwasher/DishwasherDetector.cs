using UnityEngine;
using UnityEngine.SceneManagement;

public class DishwasherDetector : MonoBehaviour
{
    [Header("Visualisers")]
    [SerializeField] private GameObject visualCue;
    private bool playerInRange;
    
    [Header("Managers")]
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private MinigameManager _minigameManager;
    
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
                //_minigameManager.MiniGameStart();
                SceneManager.LoadScene("Dishwasher", LoadSceneMode.Additive);
                MaintenanceManager.RemoveMaintenanceEvent();
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
