using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = System.Numerics.Vector2;

public class MinigameInput : MonoBehaviour
{
    
    private bool interactPressed;
    private bool submitPressed;
    private bool moveLPressed;
    private bool moveRPressed;
    private bool moveUPressed;
    private bool moveDPressed;
    
    private Vector2 mousePosition;
    
    private static MinigameInput instance;
    float buttonCoolDown;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        instance = this;
    }
    
    private void Update()
    {
        buttonCoolDown +=1*Time.deltaTime;
    }

    public static MinigameInput GetInstance() 
    {
        return instance;
    }
    
    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed&& buttonCoolDown>0.2f)
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
        if (context.performed&& buttonCoolDown>0.2f)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        } 
    }
    
    public void MoveLPressed(InputAction.CallbackContext context)
    {
        if (context.performed&& buttonCoolDown>0.2f)
        {
            moveLPressed = true;
        }
        else if (context.canceled)
        {
            moveLPressed = false;
        } 
    }
    
    public void MoveRPressed(InputAction.CallbackContext context)
    {
        if (context.performed&& buttonCoolDown>0.2f)
        {
            moveRPressed = true;
        }
        else if (context.canceled)
        {
            moveRPressed = false;
        } 
    }
    
    public void MoveUPressed(InputAction.CallbackContext context)
    {
        if (context.performed&& buttonCoolDown>0.2f)
        {
            moveUPressed = true;
        }
        else if (context.canceled)
        {
            moveUPressed = false;
        } 
    }
    
    public void MoveDPressed(InputAction.CallbackContext context)
    {
        if (context.performed&& buttonCoolDown>0.2f)
        {
            moveDPressed = true;
        }
        else if (context.canceled)
        {
            moveDPressed = false;
        } 
    }
        
    public bool GetInteractPressed() 
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetMoveLPressed() 
    {
        bool result = moveLPressed;
        moveLPressed = false;
        return result;
    }
    
    public bool GetMoveRPressed() 
    {
        bool result = moveRPressed;
        moveRPressed = false;
        return result;
    }
    
    public bool GetMoveUPressed() 
    {
        bool result = moveUPressed;
        moveUPressed = false;
        return result;
    }
    
    public bool GetMoveDPressed() 
    {
        bool result = moveDPressed;
        moveDPressed = false;
        return result;
    }

    public void RegisterSubmitPressed() 
    {
        submitPressed = false;
    }
}

