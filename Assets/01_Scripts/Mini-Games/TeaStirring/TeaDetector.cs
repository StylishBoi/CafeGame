using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class TeaDetector : MonoBehaviour
{
    
    [Header("Visualisers")]
    [SerializeField] private GameObject visualCue;
    private bool _playerInRange;
    
    [FormerlySerializedAs("uiManager")]
    [Header("Managers")]
    [SerializeField] private CafeUIManager cafeUIManager;
    [SerializeField] private MinigameManager minigameManager;
    
    private void Awake()
    {
        _playerInRange = false;
        visualCue.SetActive(false);
    }

    void Update()
    {
        if (_playerInRange && InventoryManager.Instance.slotsInUsage!=3)
        {
            visualCue.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed())
            {
                //_minigameManager.MiniGameStart();
                SceneManager.LoadScene("TeaStirring", LoadSceneMode.Additive);
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
