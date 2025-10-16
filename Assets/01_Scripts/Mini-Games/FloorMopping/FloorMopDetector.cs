using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class FloorMopDetector : MonoBehaviour
{
    [Header("Visualisers")]
    [SerializeField] private GameObject visualCue;
    private bool playerInRange;
    
    [FormerlySerializedAs("_uiManager")]
    [Header("Managers")]
    [SerializeField] private CafeUIManager cafeUIManager;
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
                SceneManager.LoadScene("FloorCleaning", LoadSceneMode.Additive);
                Destroy(gameObject);
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
