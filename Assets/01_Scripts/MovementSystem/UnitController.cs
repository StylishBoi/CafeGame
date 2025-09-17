using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private Transform selectedUnit;
    private bool unitSelected = false;
    
    GridManager _gridManager;
    
    void Start()
    {
        _gridManager=FindFirstObjectByType<GridManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit2D hasHit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hasHit)
            {
                if (hasHit.transform.CompareTag("Tile"))
                {
                    if (unitSelected)
                    {
                        Vector2Int targetCords = hasHit.transform.GetComponent<Labeller>().cords;
                        //Vector2Int startCords = new Vector2Int((int) selectedUnit.position.x, (int) selectedUnit.position.y) / _gridManager.UnitGridSize;
                        
                        selectedUnit.transform.position = new Vector3(targetCords.x, targetCords.y, selectedUnit.position.y);
                    }
                }

                if (hasHit.transform.CompareTag("Unit"))
                {
                    selectedUnit = hasHit.transform;
                    unitSelected = true;
                }
            }
        }
    }
}
