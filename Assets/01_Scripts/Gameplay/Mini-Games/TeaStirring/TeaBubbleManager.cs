using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TeaBubbleManager : MonoBehaviour
{
    [Header("Minigames Award")]
    [SerializeField] Item goodMinigameItem;
    
    [Header("Minigame")]
    [SerializeField] GameObject minigameHeader;
    
    [Header("Animation")]
    [SerializeField] private SpriteRenderer liquidTea;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    
    private List<TeaBubble> _listofbubbles = new List<TeaBubble>();
    private int _totalBubbles;
    private int _currentBubble;

    private int _totalRotations=10;
    private int _currentRotation;
    
    void Start()
    {
        _totalBubbles=gameObject.transform.childCount;
        
        TeaBubble[] targetsArray = GetComponentsInChildren<TeaBubble>();
        _listofbubbles = new List<TeaBubble>(targetsArray);
        MoveToNextBubble();
    }

    private void OnDisable()
    {
        _currentRotation = 0;
        liquidTea.color=startColor;
    }

    public void MoveToNextBubble()
    {
        LiquidAnimation();
        
        //Goes through the list after a bubble has been popped
        if (_currentBubble < _totalBubbles - 1)
        {
            _currentBubble++;
        }
        else
        {
            _currentBubble = 0;
        }
        _listofbubbles[_currentBubble].Activate();
        
        //Count the numbers of bubble popped to see if the minigame is over or not
        _currentRotation++;
        
        if (_currentRotation >= _totalRotations)
        {
            MinigameManager.Instance.MiniGameEnd();
            InventoryManager.Instance.AddItem(goodMinigameItem);
            AudioManager.Instance.PlaySfx(AudioManager.Instance.itemReceiveSFX);
            minigameHeader.SetActive(false);
        }
    }

    private void LiquidAnimation()
    {
        Debug.Log(liquidTea.color);
        liquidTea.color=Color.Lerp(startColor, endColor, (float)_currentRotation/_totalRotations);
        Debug.Log(liquidTea.color);
    }
}
