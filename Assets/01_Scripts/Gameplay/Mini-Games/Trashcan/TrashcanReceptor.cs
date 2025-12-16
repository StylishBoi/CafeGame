using System;
using UnityEngine;

public class TrashcanReceptor : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private Vector2 _movementDirection;
    [SerializeField] private float moveSpeed = 2f;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        int horizontal = MinigameInput.Instance.GetHorizontalHeld();

        _rb2d.linearVelocity = new Vector2(horizontal * moveSpeed, _rb2d.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trashbag"))
        {
            Debug.Log("Trash bag entered");
            TrashcanMinigame.trashCount++;
            Destroy(collision.gameObject);
        }
    }
}