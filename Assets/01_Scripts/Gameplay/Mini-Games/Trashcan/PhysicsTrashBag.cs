using UnityEngine;

public class PhysicsTrashBag : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed;
    [SerializeField]
    private float fallSpeed;
    void FixedUpdate()
    {
        transform.position -= transform.up * Time.deltaTime * fallSpeed;
        transform.Rotate(Vector3.forward, Time.deltaTime * spinSpeed);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionBorder"))
        {
            Destroy(gameObject);
        }
    }
}
