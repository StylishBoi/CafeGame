using System;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private bool _dragging = false;
    private Vector3 _offset;
    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void OnDisable()
    {
        _dragging = false;
        transform.position = _startPosition;
    }
    
    void Update()
    {
        if (_dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + _offset;
        }
    }
    private void OnMouseDown()
    {
        _offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _dragging = true;
    }

    private void OnMouseUp()
    {
        _dragging = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        
        if (collision.CompareTag("Dishwasher"))
        {
            gameObject.SetActive(false);
            _dragging = false;
        }
        
        else if (collision.gameObject.CompareTag("CollisionBorder"))
        {
            _dragging = false;
            transform.position = _startPosition;
        }
    }
}
