using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private Transform selectedUnit;
    private bool unitSelected = false;
    
    List<Node> path = new List<Node>();
    
    GridManager _gridManager;
    TilePathfinding _tilePathfinding;
    
    void Start()
    {
        _gridManager=FindFirstObjectByType<GridManager>();
        _tilePathfinding=FindFirstObjectByType<TilePathfinding>();
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
                        Vector2Int targetCords = hasHit.transform.GetComponent<Tile>().cords;
                        Vector2Int startCords = new Vector2Int((int) selectedUnit.transform.position.x, (int) selectedUnit.transform.position.y) / _gridManager.UnitGridSize;
                        _tilePathfinding.SetNewDestination(startCords, targetCords);
                        RecalculatePath(true);
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
        path.Clear();
        path=_tilePathfinding.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition=selectedUnit.position;
            Vector3 endPosition = _gridManager.GetPositionFromCoordinates(path[i].Cords);
            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                selectedUnit.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
