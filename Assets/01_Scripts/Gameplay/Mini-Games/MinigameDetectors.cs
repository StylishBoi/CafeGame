using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine;

public class MinigameDetector : MonoBehaviour
{
    [Header("Visualisers")]
    [SerializeField] private GameObject visualCue;
    private bool playerInRange;
    
    [Header("Managers")]
    [SerializeField] private CafeUIManager cafeUIManager;

    public enum MinigameScenes
    {
        PouringCafe,
        Dishwasher,
        FloorCleaning,
        ShakeMilkshake,
        TeaStirring,
        Trashcan
    }
    
    public MinigameScenes minigameScene;
    
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
                SceneManager.LoadScene(minigameScene.ToString(), LoadSceneMode.Additive);
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
