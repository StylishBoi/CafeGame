using UnityEngine;

[ExecuteAlways]
public class Labeller : MonoBehaviour
{
    public Vector2Int cords=new Vector2Int();
    private GridManager _gridManager;
    
    void Awake()
    {
        _gridManager = FindFirstObjectByType<GridManager>();
    }

    private void Update()
    {
        DisplayCords();
        transform.name=cords.ToString();
    }

    private void DisplayCords()
    {
        if(!_gridManager) { return; }
        
        cords.x=Mathf.RoundToInt(transform.position.x/_gridManager.UnitGridSize);
        cords.y=Mathf.RoundToInt(transform.position.y/_gridManager.UnitGridSize);

    }
}
