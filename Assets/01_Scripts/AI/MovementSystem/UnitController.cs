using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private Transform _selectedUnit;
    
    List<Node> _path = new List<Node>();
    
    GridManager _gridManager;
    TilePathfinding _tilePathfinding;
    
    void Awake()
    {
        _gridManager=FindFirstObjectByType<GridManager>();
        _tilePathfinding=FindFirstObjectByType<TilePathfinding>();
        _selectedUnit = transform;
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = _tilePathfinding.StartCords;
        }
        else
        {
            coordinates=_gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        _path.Clear();
        _path=_tilePathfinding.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < _path.Count; i++)
        {
            Vector3 startPosition=_selectedUnit.position;
            Vector3 endPosition = _gridManager.GetPositionFromCoordinates(_path[i].Cords);
            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                _selectedUnit.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void NPCInfo(Vector2Int NpcPosition, Vector2Int TargetPosition, Transform NPC)
    {
        Vector2Int targetCords = TargetPosition;
        Vector2Int startCords = NpcPosition;
        _selectedUnit = NPC;
        _tilePathfinding.SetNewDestination(startCords, targetCords);
        RecalculatePath(true);
    }
}
