using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    
    private bool interactPressed = false;
    private bool submitPressed = false;
    
    private static InputManager instance;
    
    float buttonCoolDown;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        
        _moveAction = _playerInput.actions["Move"];
        
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;
    }

    public static InputManager GetInstance() 
    {
        return instance;
    }
    
    private void Update()
    {
        Movement=_moveAction.ReadValue<Vector2>();

        if (buttonCoolDown < 0.5f)
        {
            buttonCoolDown +=Time.deltaTime;
        }

        if (SceneManager.sceneCount!=1)
        {
            _playerInput.enabled = false;
        }
        else
        {
            _playerInput.enabled = true;
        }
    }
    
    public void InteractButtonPressed(InputAction.CallbackContext context)
        {
            if (context.performed && buttonCoolDown>0.5f)
            {
                interactPressed = true;
            }
            else if (context.canceled)
            {
                interactPressed = false;
            } 
        }
    
        public void SubmitPressed(InputAction.CallbackContext context)
        {
            if (context.performed && buttonCoolDown>0.5f)
            {
                submitPressed = true;
            }
            else if (context.canceled)
            {
                submitPressed = false;
            } 
        }
        
        public bool GetInteractPressed() 
        {
            bool result = interactPressed;
            interactPressed = false;
            return result;
        }

        public bool GetSubmitPressed() 
        {
            bool result = submitPressed;
            submitPressed = false;
            return result;
        }

        public void RegisterSubmitPressed() 
        {
            submitPressed = false;
        }
}
