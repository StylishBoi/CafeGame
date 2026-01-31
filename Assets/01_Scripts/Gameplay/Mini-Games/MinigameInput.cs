using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameInput : MonoBehaviour
{
    public static MinigameInput Instance { get; private set; }
    
    private bool interactPressed;
    private bool submitPressed;
    private bool moveLPressed;
    private bool moveRPressed;
    private bool moveUPressed;
    private bool moveDPressed;
    private bool moveLHeld;
    private bool moveRHeld;
    
    private Vector2 mousePosition;
    
    private float horizontalInput;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        Instance = this;
    }

    private void HandleButton(InputAction.CallbackContext context, ref bool flag)
    {
        if (context.started)
            flag = true;
    }
    
    
    public void Interact(InputAction.CallbackContext context)
        => HandleButton(context, ref interactPressed);

    public void Submit(InputAction.CallbackContext context)
        => HandleButton(context, ref submitPressed);

    public void MoveLeft(InputAction.CallbackContext context)
        => HandleButton(context, ref moveLPressed);

    public void MoveRight(InputAction.CallbackContext context)
        => HandleButton(context, ref moveRPressed);

    public void MoveUp(InputAction.CallbackContext context)
        => HandleButton(context, ref moveUPressed);

    public void MoveDown(InputAction.CallbackContext context)
        => HandleButton(context, ref moveDPressed);
    
    public void MoveHorizontal(InputAction.CallbackContext context)
    {
        Debug.Log("Move Horizontal has been read");
        horizontalInput = context.ReadValue<float>();
    }
    
    public int GetHorizontalHeld()
    {
        return (moveRHeld ? 1 : 0) - (moveLHeld ? 1 : 0);
    }
    
    private bool Consume(ref bool value)
    {
        bool result = value;
        value = false;
        return result;
    }

    public bool GetInteractPressed() => Consume(ref interactPressed);
    public bool GetSubmitPressed()   => Consume(ref submitPressed);
    public bool GetMoveLPressed()    => Consume(ref moveLPressed);
    public bool GetMoveRPressed()    => Consume(ref moveRPressed);
    public bool GetMoveUPressed()    => Consume(ref moveUPressed);
    public bool GetMoveDPressed()    => Consume(ref moveDPressed);
}

