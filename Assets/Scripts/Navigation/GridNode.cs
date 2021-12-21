using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class GridNode
{
    public GridNode(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        _walkable = walkable;
        _worldPosition = worldPosition;
        _gridPosition = new Vector2Int(gridX, gridY);
    }
    public bool Walkable { get => _walkable; set { _walkable = value; } }
    private bool _walkable;
    public Vector3 WorldPosition { get => _worldPosition; set { _worldPosition = value; } }
    private Vector3 _worldPosition;
    public Vector2Int GridPosition { get => _gridPosition; set { _gridPosition = value; } }
    private Vector2Int _gridPosition;

    public int GCost{ get => _gCost; set { _gCost = value; } }
    private int _gCost;
    public int HCost{ get => _hCost; set { _hCost = value; } }
    private int _hCost;
    public int FCost => _hCost + _gCost;

    public GridNode Parent;
}
