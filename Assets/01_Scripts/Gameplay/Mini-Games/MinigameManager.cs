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
    
    private PlayerMovement _playerMovement;
    private GameObject _deletedObject;

    private void Awake()
    {
        if (Instance && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
    
    public void MiniGameEnd()
    {
        CafeUIManager.Instance.NewItem();
        ExitMinigame();
    }

    public void EnterMinigame()
    {
        InputManager.Instance.playerInput.SwitchCurrentActionMap("Minigame");
        _currentMinigameState = MinigameState.InGame;
    }
    public void EnterMinigame(GameObject minigame)
    {
        InputManager.Instance.playerInput.SwitchCurrentActionMap("Minigame");
        _deletedObject = minigame;
        _currentMinigameState = MinigameState.InGame;
    }

    private void ExitMinigame()
    {
        InputManager.Instance.playerInput.SwitchCurrentActionMap("Movement");
        if (_deletedObject != null)
        {
            Destroy(_deletedObject);
        }
        _currentMinigameState = MinigameState.OutGame;
    }
}
