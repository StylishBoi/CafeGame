using UnityEngine.SceneManagement;
using UnityEngine;

public class MinigameDetector : MonoBehaviour
{
    [Header("Visualisers")] [SerializeField]
    private GameObject visualCue;

    private bool _playerInRange;

    [Header("Minigame")] [SerializeField] private GameObject minigamePrefab;
    [SerializeField] private bool deleteableMinigame;
    [SerializeField] private bool dishwasher;

    private void Awake()
    {
        _playerInRange = false;
        visualCue.SetActive(false);
    }

    void Update()
    {
        if (dishwasher && MaintenanceManager.dishwasherState !=
            MaintenanceManager.WashingMachineState.NeedsCleaning)
        {
            return;
        }
        if (_playerInRange && InventoryManager.Instance.slotsInUsage != 3)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed() && (GameManager.Instance.State == GameState.CafePlay ||
                                                                    GameManager.Instance.State == GameState.BasicPlay))
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
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}