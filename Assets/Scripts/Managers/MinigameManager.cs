using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private GameObject inputManager;
    [SerializeField] private PlayerMovement playerMovement;
    
    
    private void Start()
    {
        inputManager.SetActive(true);
    }
    public static void MiniGameEnd()
    {
        SceneManager.LoadScene("CafePrototype");
    }

    public void MiniGameStart()
    {
        /*playerMovement.StopMovement();
        inputManager.SetActive(false);
        playerMovement.StopMovement();*/
    }
    
    //Make it so character is unable to move while in minigame

}
