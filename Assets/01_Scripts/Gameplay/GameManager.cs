using UnityEngine;

public enum GameState
{
    Menu,
    CafePlay,
    BasicPlay,
    Paused,
    Cinematic,
    TextBox
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameState _currentGameState;
    private GameState _lastGameState;

    public GameState State => _currentGameState;

    private void Awake()
    {
        if (Instance && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void SwitchState(GameState newState)
    {
        if (newState == _currentGameState)
        {
            Debug.LogWarning($"Already in {_currentGameState} state, cannot change");
            return;
        }

        _currentGameState = newState;
        Debug.Log($"Game State: {_currentGameState}");
    }

    #region Menu

    public void StartGame()
    {
        SwitchState(GameState.Cinematic);
    }

    #endregion

    #region CafePlay

    public void StartDay()
    {
        SwitchState(GameState.CafePlay);
    }

    public void EndCafeDay()
    {
        SwitchState(GameState.BasicPlay);
    }
    public void EndFullDay()
    {
        SwitchState(GameState.Cinematic);
    }

    #endregion

    #region Paused

    public void Pause()
    {
        _lastGameState = _currentGameState;
        SwitchState(GameState.Paused);
    }

    public void UnPause()
    {
        SwitchState(_lastGameState);
    }

    #endregion

    #region Cinematic

    public void SendToMenu()
    {
        SwitchState(GameState.Menu);
    }

    #endregion
}