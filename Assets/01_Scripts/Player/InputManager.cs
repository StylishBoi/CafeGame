using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;

    public PlayerInput playerInput;
    private InputAction _moveAction;

    //Buttons pressed
    private bool _interactPressed;
    private bool _submitPressed;

    public static InputManager Instance;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        _moveAction = playerInput.actions["Move"];

        if (Instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }

        Instance = this;
    }

    public static InputManager GetInstance()
    {
        return Instance;
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();
    }

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _interactPressed = true;
        }
        else if (context.canceled)
        {
            _interactPressed = false;
        }
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _submitPressed = true;
        }
        else if (context.canceled)
        {
            _submitPressed = false;
        }
    }

    public void PausePressed(InputAction.CallbackContext context)
    {
        if (context.started && (GameManager.Instance.State == GameState.BasicPlay ||
            GameManager.Instance.State == GameState.CafePlay))
        {
            PauseMenu.Instance.PauseGame();
        }
    }

    public void UnpausePressed(InputAction.CallbackContext context)
    {
        if (context.started && GameManager.Instance.State == GameState.Paused)
        {
            PauseMenu.Instance.UnpauseGame();
        }
    }

    public bool GetInteractPressed()
    {
        bool result = _interactPressed;
        _interactPressed = false;
        return result;
    }

    public bool GetSubmitPressed()
    {
        bool result = _submitPressed;
        _submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed()
    {
        _submitPressed = false;
    }
}