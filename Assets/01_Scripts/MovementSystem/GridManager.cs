using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] private int unitGridSize;
    
    public int UnitGridSize {get { return unitGridSize; } }
    
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].Walkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.ConnectTo = null;
            entry.Value.Explored = false;
            entry.Value.Path = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int gridCoordinates = new Vector2Int();
        
        gridCoordinates.x = Mathf.RoundToInt(position.x / unitGridSize);
        gridCoordinates.y = Mathf.RoundToInt(position.y / unitGridSize);

        return gridCoordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        
        position.x = coordinates.x * unitGridSize;
        position.y = coordinates.y * unitGridSize;
        
        return position;
    }

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int cords = new Vector2Int(x, y);
                grid.Add(cords, new Node(cords, true));
            }
        }
        Debug.Log(grid.Keys.Count);
    }
}
