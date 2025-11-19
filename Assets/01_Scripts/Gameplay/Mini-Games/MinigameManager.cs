using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    private GameObject _inputManager;
    private PlayerMovement _playerMovement;

    public static void MiniGameEnd()
    {
        var lastSceneIndex = SceneManager.sceneCount - 1;
        
        var LastLoadedScene = SceneManager.GetSceneAt(lastSceneIndex);
        
        SceneManager.UnloadSceneAsync(LastLoadedScene);
        
        CafeUIManager.Instance.NewItem();
    }
}
