using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 _movement;

    private Rigidbody2D _rb;
    private Animator _animator;
    
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        /*if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }*/
        
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        
        _rb.linearVelocity = _movement * moveSpeed;
        
        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(_lastHorizontal, _movement.x);
            _animator.SetFloat(_lastVertical, _movement.y);
        }
    }

    public void StopMovement()
    {
        Debug.Log("Movement stopped");
        _movement.Set(0, 0);
        _animator.SetFloat(_horizontal, 0);
        _animator.SetFloat(_vertical, 0);
    }
}
