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
            Debug.Log(Input.mousePosition);
            RaycastHit hit;
            
            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                if (hit.transform.tag == "Tile")
                {
                    if (unitSelected)
                    {
                        Vector2Int targetCords = hit.transform.GetComponent<Labeller>().cords;
                        Vector2Int startCords = new Vector2Int((int) selectedUnit.position.x, (int) selectedUnit.position.y) / _gridManager.UnitGridSize;
                        
                        selectedUnit.transform.position = new Vector3(targetCords.x, selectedUnit.position.y, targetCords.y);
                    }
                }

                if (hit.transform.tag == "Unit")
                {
                    Debug.Log("Unit has been selected");
                    selectedUnit = hit.transform;
                    unitSelected = true;
                }
            }
        }
    }
}
