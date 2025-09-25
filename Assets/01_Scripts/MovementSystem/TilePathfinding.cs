using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TilePathfinding : MonoBehaviour
{
    [SerializeField] private Vector2Int startCords;

    public Vector2Int StartCords { get { return startCords; } }

    [SerializeField] private Vector2Int targetCords;

    public Vector2Int TargetCords { get { return targetCords; } }

    Node _startNode;
    Node _targetNode;
    Node _currentNode;

    Queue<Node> _frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();
    
    GridManager _gridManager;
    Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();
    
    Vector2Int[] _searchOrder={Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};

    private void Start()
    {
        _gridManager = FindFirstObjectByType<GridManager>();
        if (_gridManager != null)
        {
            Debug.Log("GridManager found");
            _grid=_gridManager.Grid;
        }
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCords);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        _gridManager.ResetNodes();

        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        _startNode.Walkable = true;
        _targetNode.Walkable = true;
        
        _frontier.Clear();
        _reached.Clear();

        bool isRunning = true;
        
        _frontier.Enqueue(_grid[coordinates]);
        _reached.Add(coordinates, _grid[coordinates]);

        while (_frontier.Count > 0 && isRunning)
        {
            _currentNode = _frontier.Dequeue();
            _currentNode.Explored = true;
            ExploreNeighbors();
            if (_currentNode.Cords==targetCords)
            {
                isRunning = false;
                _currentNode.Explored = false;
            }
        }
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in _searchOrder)
        {
            Vector2Int neighborCoords = _currentNode.Cords + direction;

            if (_grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(_grid[neighborCoords]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!_reached.ContainsKey(neighbor.Cords) && neighbor.Walkable)
            {
                neighbor.ConnectTo=_currentNode;
                _reached.Add(neighbor.Cords, neighbor);
                _frontier.Enqueue(neighbor);
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = _targetNode;
        
        path.Add(currentNode);
        currentNode.Path = true;

        while (currentNode.ConnectTo != null)
        {
            currentNode = currentNode.ConnectTo;
            path.Add(currentNode);
            currentNode.Path = true;
        }
        
        path.Reverse();
        return path;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

    public void SetNewDestination(Vector2Int startCoordinates, Vector2Int targetCoordinates)
    {
        startCords = startCoordinates;
        targetCords = targetCoordinates;
        Debug.Log("Stop 1");
        Debug.Log(_grid.Count);
        Debug.Log(_grid.Keys.First());
        Debug.Log("Stop 2");
        _startNode = _grid[startCords];
        _targetNode = _grid[targetCords];
        GetNewPath();
    }
}
