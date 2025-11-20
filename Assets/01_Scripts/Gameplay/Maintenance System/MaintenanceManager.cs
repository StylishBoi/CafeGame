using UnityEngine;
using System.Collections.Generic;

public class MaintenanceManager : MonoBehaviour
{
    [Header("Maintenance Spawn")]
    [SerializeField] private List<GameObject> maintenanceEvents;
    [SerializeField] private LayerMask unSpawnableLayers;
    [SerializeField] private Collider2D spawnableAreaCollider;
    [SerializeField] private Vector2 dishwasherSpawn;
    
    [Header("Maintenance Time")]
    [SerializeField] private int minMaintenanceTime = 15;
    [SerializeField] private int maxMaintenanceTime = 30;
    private float _timerForNextMaintenanceEvent;
    private int _nextMaintenanceEvent;
    
    public static List<GameObject> CurrentMaintenanceEvents = new List<GameObject>(); 

    void Update()
    {
        if (_timerForNextMaintenanceEvent < _nextMaintenanceEvent)
        {
            _timerForNextMaintenanceEvent += Time.deltaTime;
        }
        else if (GameManager.Instance.State==GameState.CafePlay)
        {
            CreateMaintenanceEvent();
        }
    }

    private void CreateMaintenanceEvent()
    {
        //Decide which maintenance event it will be
        int random = Random.Range(0, maintenanceEvents.Count);

        if (random == 0)
        {
            //Put the maintenance event on the map
            Vector2 spawnPosition = GetRandomSpawnPosition(spawnableAreaCollider);
            if (spawnPosition != Vector2.zero)
            {
                var newMaintenanceEvent = Instantiate(maintenanceEvents[random], spawnPosition, Quaternion.identity, transform);
                CurrentMaintenanceEvents.Add(newMaintenanceEvent);
            }
        }

        if (random == 1)
        {
            var newMaintenanceEvent = Instantiate(maintenanceEvents[random], dishwasherSpawn, Quaternion.identity, transform);
            CurrentMaintenanceEvents.Add(newMaintenanceEvent);
        }
        
        //Set stats for next time
        _nextMaintenanceEvent=Random.Range(minMaintenanceTime, maxMaintenanceTime);
        _timerForNextMaintenanceEvent = 0;
    }

    private Vector2 GetRandomSpawnPosition(Collider2D spawnableAreaCollider)
    {
        var spawnPosition = Vector2.zero;
        var isSpawnPosValid = false;
        
        var attemptCount = 0;
        const int maxAttempts = 200;

        while (!isSpawnPosValid && attemptCount < maxAttempts)
        {
            spawnPosition=GetRandomPointInCollider(spawnableAreaCollider);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 0.5f);
            
            var isInvalidCollision = false;
            foreach (Collider2D collider in colliders)
            {
                if (((1<<collider.gameObject.layer) & unSpawnableLayers) != 0)
                {
                    isInvalidCollision = true;
                    break;
                }
            }

            if (!isInvalidCollision)
            {
                isSpawnPosValid = true;
            }
            attemptCount++;
        }

        if (!isSpawnPosValid)
        {
            Debug.LogWarning("No valid position left");
            return Vector2.zero;
        }
        return spawnPosition;
    }

    private Vector2 GetRandomPointInCollider(Collider2D collider, float offset = 1f)
    {
        var collBounds = collider.bounds;
        
        var minBounds= new Vector2(collBounds.min.x + offset, collBounds.min.y + offset);
        var maxBounds= new Vector2(collBounds.max.x - offset, collBounds.max.y - offset);
        
        var randomX = Random.Range(minBounds.x, maxBounds.x);
        var randomY = Random.Range(minBounds.y, maxBounds.y);
        
        return new Vector2(randomX, randomY);
    }

    public static void RemoveMaintenanceEvent()
    {
        CurrentMaintenanceEvents.RemoveAt(0);
        foreach (var maintenanceEvent in CurrentMaintenanceEvents)
        {
            
            Debug.Log(maintenanceEvent);
            if (maintenanceEvent == null)
            {
                //CurrentMaintenanceEvents.Remove(maintenanceEvent);
            }
        }
    }
}

//1 - Fix log of current maintenance events
//2 - Create an effect controller for the restaurant
//3 - Make a UI for the effect
//4 - Clean-Up Code
//5 - Add more mini-games