using System.Collections;
using UnityEngine;

public class TeaBubbleManager : MonoBehaviour
{
    [Header("Minigames Award")] [SerializeField]
    Item goodMinigameItem;

    [Header("Minigame")] [SerializeField] GameObject minigameHeader;
    [SerializeField] GameObject teaBubble;
    [SerializeField] private Collider2D spawnableAreaCollider;

    [Header("Animation")] [SerializeField] private SpriteRenderer liquidTea;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    private int _totalRotations = 10;
    private int _currentRotation;

    void OnEnable()
    {
        MoveToNextBubble();
    }

    private void OnDisable()
    {
        _currentRotation = 0;
        liquidTea.color = startColor;
    }

    public void MoveToNextBubble()
    {
        LiquidAnimation();

        //Count the numbers of bubble popped to see if the minigame is over or not
        _currentRotation++;

        Vector2 spawnPosition = GetRandomSpawnPosition(spawnableAreaCollider);
        if (spawnPosition != Vector2.zero)
        {
            var newMaintenanceEvent = Instantiate(teaBubble, spawnPosition, Quaternion.identity, transform);
        }

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
        liquidTea.color = Color.Lerp(startColor, endColor, (float)_currentRotation / _totalRotations);
        Debug.Log(liquidTea.color);
    }

    private Vector2 GetRandomSpawnPosition(Collider2D spawnableAreaCollider)
    {
        var spawnPosition = Vector2.zero;
        var isSpawnPosValid = false;

        var attemptCount = 0;
        const int maxAttempts = 200;

        while (attemptCount < maxAttempts)
        {
            spawnPosition = GetRandomPointInCollider(spawnableAreaCollider);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPosition, 0.5f);
            
            var isInvalidCollision = false;
            foreach (Collider2D collider in colliders)
            {
                if (((1<<collider.gameObject.layer)) != 0)
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

        // if (!isSpawnPosValid)
        // {
        //     Debug.LogWarning("No valid position left");
        //     return Vector2.zero;
        // }
        return spawnPosition;
    }

    private Vector2 GetRandomPointInCollider(Collider2D collider, float offset = 1f)
    {
        var collBounds = collider.bounds;

        var minBounds = new Vector2(collBounds.min.x + offset, collBounds.min.y + offset);
        var maxBounds = new Vector2(collBounds.max.x - offset, collBounds.max.y - offset);

        var randomX = Random.Range(minBounds.x, maxBounds.x);
        var randomY = Random.Range(minBounds.y, maxBounds.y);

        return new Vector2(randomX, randomY);
    }
}