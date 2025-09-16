using UnityEngine;

[ExecuteAlways]
public class Labeller : MonoBehaviour
{
    public Vector2Int cords=new Vector2Int();
    GridManager gridManager;
    
    void Awake()
    {
        gridManager = FindFirstObjectByType<GridManager>();
    }

    private void Update()
    {
        DisplayCords();
        transform.name=cords.ToString();
    }

    private void DisplayCords()
    {
        if(!gridManager) { return; }
        
        cords.x=Mathf.RoundToInt(transform.position.x/gridManager.UnitGridSize);
        cords.y=Mathf.RoundToInt(transform.position.y/gridManager.UnitGridSize);

    }
}
