using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellState
{
    EMPTY, //cell has nothing in it
    MARKED, // cell will have somthing moving into it next turn
    CONFLICTED, // cell has multiple things assigned to it
    OCCUPIED // cell has a blocking object within it
}

[System.Serializable]
public class GridCell 
{
    public GridCell(GameObject occupier, CellState state, Vector3 worldPosition, int gridX, int gridY)
    {
        Occupier = occupier;
        State = state;
        _worldPosition = worldPosition;
        _gridPosition = new Vector2Int(gridX, gridY);
    }
    public GameObject Occupier { get; set; }
    public CellState State { get; set ; }
    public Vector3 WorldPosition { get => _worldPosition; private set { _worldPosition = value; } }
    private Vector3 _worldPosition;
    public Vector2Int GridPosition { get => _gridPosition; private set { _gridPosition = value; } }
    private Vector2Int _gridPosition;

    // pathfinding variables
    public int GCost { get => _gCost; set { _gCost = value; } }
    private int _gCost;
    public int HCost { get => _hCost; set { _hCost = value; } }
    private int _hCost;
    public int FCost => _hCost + _gCost;

    public GridCell Parent;

}
