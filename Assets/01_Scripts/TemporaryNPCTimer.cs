using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryNPCTimer : MonoBehaviour
{
    private SpriteRenderer _timerSprite;
    private float totalRadius = 0f;
    
    private float waitTimer = 5;

    void Start()
    {
        _timerSprite = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        totalRadius += (Time.deltaTime*360f)/waitTimer;
        
        _timerSprite.material.SetFloat("_Arc1", totalRadius);
    }

}
