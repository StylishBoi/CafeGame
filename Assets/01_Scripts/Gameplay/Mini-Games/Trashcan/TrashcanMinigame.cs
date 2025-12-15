using UnityEngine;

public class TrashcanMinigame : MonoBehaviour
{
    [SerializeField]
    private GameObject trashBag;
    [SerializeField]
    private Collider2D trashZone;
    
    public static int trashCount = 0;
    
    void Start()
    {
        Instantiate(trashBag, GetRandomPointInCollider(trashZone), Quaternion.identity, transform);
        trashCount = 0;
    }

    void Update()
    {
        if (transform.childCount == 0)
        {
            Instantiate(trashBag, GetRandomPointInCollider(trashZone), Quaternion.identity, transform);
        }

        if (trashCount >= 3)
        {
            MinigameManager.MiniGameEnd();
        }
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
}
