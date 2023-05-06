using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public List<Node> Path;
    [SerializeField] private LayerMask _unwalkableLayer;
    [SerializeField] private Vector2 _gridWorldSize;
    [SerializeField] private float _nodeRadius;
    private float _nodeDiameter;
    private int _gridSizeX, _gridSizeY;
    private Node[,] _grid;

    private void Awake()
    {
        _nodeDiameter = _nodeRadius * 2;
        _gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);
        CreateGrid();
    }
    public Node NodeFromWorldPoint(Vector2 worldPoint)
    {
        float percentX = (worldPoint.x + _gridWorldSize.x * .5f) / _gridWorldSize.x;
        float percentY = (worldPoint.y + _gridWorldSize.y * .5f) / _gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);
        return _grid[x, y];
    }
    public List <Node> GetNeighbours(Node node)
    {
        List <Node> neighbours = new();
        for (int x = -1; x <= 1; x++)
        { 
            for (int y = -1;  y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = node.GridX + x;
                int checkY = node.GridY + y;
                if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                {
                    neighbours.Add(_grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }
    private void CreateGrid()
    {
        _grid = new Node[_gridSizeX,_gridSizeY];
        Vector2 worldBottomLeft = (Vector2)transform.position - (Vector2.right * _gridWorldSize.x * .5f) - (Vector2.up * _gridWorldSize.y *.5f); 
        for (int x = 0; x < _gridSizeX; x++)
        {
            for (int y = 0; y < _gridSizeY; y++)
            {
                Vector2 currentWorldPoint = worldBottomLeft + Vector2.right * (x * _nodeDiameter + _nodeRadius) + Vector2.up * (y * _nodeDiameter + _nodeRadius);
                _grid[x, y] = new Node(!Physics2D.Raycast(currentWorldPoint,Vector2.zero, 1, _unwalkableLayer), currentWorldPoint, x, y);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, _gridWorldSize.y, 1));
        if (_grid != null)
        { 
            foreach (Node node in _grid)
            {
                Gizmos.color = node.IsWalkable ? Color.green : Color.red;
                if (Path != null)
                {
                    if (Path.Contains(node))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(node.Position, Vector2.one * (_nodeDiameter - .1f));
            }
        }
    }
}
