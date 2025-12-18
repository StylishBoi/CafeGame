using UnityEngine;
using UnityEngine.SceneManagement;


public enum MinigameState
{
    InGame,
    OutGame
}


public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance { get; private set; }
    
    private MinigameState _currentMinigameState;
    
    public MinigameState State => _currentMinigameState;
    
    private GameObject _inputManager;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        if (Instance && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
    
    public void MiniGameEnd()
    {
        var lastSceneIndex = SceneManager.sceneCount - 1;
        
        var LastLoadedScene = SceneManager.GetSceneAt(lastSceneIndex);
        
        SceneManager.UnloadSceneAsync(LastLoadedScene);
        
        CafeUIManager.Instance.NewItem();
        ExitMinigame();
    }

    public void EnterMinigame()
    {
        _currentMinigameState = MinigameState.InGame;
    }

    private void ExitMinigame()
    {
        _currentMinigameState = MinigameState.OutGame;
    }
}
