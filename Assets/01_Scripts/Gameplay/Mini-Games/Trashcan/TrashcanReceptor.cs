using UnityEngine;

public class TrashcanReceptor : MonoBehaviour
{
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
