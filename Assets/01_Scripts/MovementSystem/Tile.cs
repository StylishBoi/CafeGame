using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool blocked;
    public Vector2Int cords;
    
    GridManager _gridManager;

    void Start()
    {
        SetCords();

        if (blocked)
        {
            _gridManager.BlockNode(cords);
        }
    }

    private void SetCords()
    {
        _gridManager=FindFirstObjectByType<GridManager>();
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        
        cords = new Vector2Int(x / _gridManager.UnitGridSize, y / _gridManager.UnitGridSize);
    }
}
