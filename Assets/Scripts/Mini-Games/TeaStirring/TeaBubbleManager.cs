using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TeaBubbleManager : MonoBehaviour
{
    public List<TeaBubble> _listofbubbles = new List<TeaBubble>();
    public int _totalBubbles;
    public int _currentBubble;

    public int _totalRotations=10;
    public int _currentRotation;
    
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
        
        Debug.Log(_currentBubble);
        
        //Count the numbers of bubble popped to see if the minigame is over or not
        _currentRotation++;
        
        if (_currentRotation >= _totalRotations)
        {
            MinigameManager.MiniGameEnd();
        }
    }
}
