using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrashcanMinigame : MonoBehaviour
{
    [SerializeField] GameObject minigameHeader;
    
    [SerializeField]
    private List<Image> trashcanIcons;
    [SerializeField]
    private GameObject trashBag;
    [SerializeField]
    private Collider2D trashZone;
    
    public static int TrashCount = 0;
    
    void Start()
    {
        Instantiate(trashBag, GetRandomPointInCollider(trashZone), Quaternion.identity, transform);
        TrashCount = 0;
    }

    void OnDisable()
    {
        Reset();
    }

    void Update()
    {
        if (transform.childCount == 0)
        {
            Instantiate(trashBag, GetRandomPointInCollider(trashZone), Quaternion.identity, transform);
        }

        if (TrashCount >= 3)
        {
            StartCoroutine(MinigameLeave());
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
    
    IEnumerator MinigameLeave()
    {
        yield return new WaitForSeconds(0.5f);
        
        //MinigameManager.Instance.MiniGameEnd();
        minigameHeader.SetActive(false);
    }

    void Reset()
    {
        TrashCount = 0;
    }
}
