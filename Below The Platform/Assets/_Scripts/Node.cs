using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool IsWalkable;
    public Vector2 Position;
    public int GridX, GridY, GCost, HCost;
    public Node Parent;
    public int FCost
    {
        get { return GCost + HCost; } 
    }
    public Node(bool isWalkable, Vector2 position, int _gridX, int _gridY)
    {
        IsWalkable = isWalkable;
        Position = position;
        GridX = _gridX;
        GridY = _gridY;
    }
}
