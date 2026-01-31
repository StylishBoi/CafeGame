using UnityEngine;

public class DishwasherDetector : MonoBehaviour
{
    [Header("Visualisers")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private GameObject warningCue;

    [Header("Minigame")]
    [SerializeField] private GameObject minigamePrefab;

    private bool _playerInRange;

    private void Awake()
    {
        _playerInRange = false;
        visualCue.SetActive(false);
        warningCue.SetActive(false);
    }

    void Update()
    {
        if (MaintenanceManager.dishwasherState == MaintenanceManager.WashingMachineState.NeedsCleaning)
        {
            warningCue.SetActive(true);
            if (_playerInRange)
            {
                visualCue.SetActive(true);
                if (InputManager.GetInstance().GetInteractPressed() &&
                    (GameManager.Instance.State == GameState.CafePlay ||
                     GameManager.Instance.State == GameState.BasicPlay))
                {
                    minigamePrefab.SetActive(true);
                    MinigameManager.Instance.EnterMinigame();
                }
            }
            else
            {
                visualCue.SetActive(false);
            }
        }
        else
        {
            warningCue.SetActive(false);
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