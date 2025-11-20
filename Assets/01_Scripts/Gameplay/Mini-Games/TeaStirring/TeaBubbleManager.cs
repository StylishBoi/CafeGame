using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TeaBubbleManager : MonoBehaviour
{
    [Header("Minigames Award")]
    [SerializeField] Item goodMinigameItem;
    
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

    public void MoveToNextBubble()
    {
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
            MinigameManager.MiniGameEnd();
            InventoryManager.Instance.AddItem(goodMinigameItem);
        }
    }
}
