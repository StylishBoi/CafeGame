using UnityEngine;

public class Node
{
    public Vector2Int Cords;
    public bool Walkable;
    public bool Explored;
    public bool Path;
    public Node ConnectTo;

    public Node(Vector2Int cords, bool walkable)
    {
        this.Cords = cords;
        this.Walkable = walkable;
    }
}
