using UnityEngine;
using UnityEngine.SceneManagement;

public class Detector : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    
    private bool playerInRange;
    private bool Minigamephase;
    
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            visualCue.SetActive(true);
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
