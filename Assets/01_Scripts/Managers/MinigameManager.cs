using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    private GameObject _inputManager;
    private PlayerMovement _playerMovement;
    
    public static void MiniGameEnd()
    {
        // Get count of loaded Scenes and last index
        var lastSceneIndex = SceneManager.sceneCount - 1;

        // Get last Scene by index in all loaded Scenes
        var lastLoadedScene = SceneManager.GetSceneAt(lastSceneIndex);

        // Unload Scene
        SceneManager.UnloadSceneAsync(lastLoadedScene);

        CafeUIManager.Instance.NewItem();
    }

    public static void MiniGameStart(Item badResult, Item goodResult)
    {
        //playerMovement.StopMovement();
        //inputManager.SetActive(false);
        //playerMovement.StopMovement();
    }
    
    //Make it so character is unable to move while in minigame

}
