using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavGridVolume : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridExtent = new Vector2Int(10, 10);
    [SerializeField] private float _cellSize = 1f;
    [SerializeField] private LayerMask _unwalkable;
    private GridCell[,] _grid;
    private Vector2Int _gridSize;
    [SerializeField] private RuntimeNavGrid _runtimeNavGrid;
    [SerializeField] private PathVariable _pathVariable;

    // Start is called before the first frame update
    void Awake()
    {
        _gridSize = new Vector2Int(Mathf.RoundToInt(_gridExtent.x / _cellSize), Mathf.RoundToInt(_gridExtent.y / _cellSize));
        CreateGrid();
        _runtimeNavGrid.SetUpGrid(_grid, _gridExtent,_gridSize);
    }

    private void CreateGrid()
    {
        _grid = new GridCell[_gridSize.x, _gridSize.y];
        Vector3 gridBottomLeft = transform.position - Vector3.right * _gridSize.x / 2 - Vector3.forward * _gridSize.y / 2;

        for (int i = 0; i < _gridSize.x; i++)
        {
            for (int j = 0; j < _gridSize.y; j++)
            {
                Vector3 worldPosition = gridBottomLeft + (Vector3.right * (_cellSize * i + _cellSize / 2)) + (Vector3.forward * (_cellSize * j + _cellSize / 2));
                bool walkable = !(Physics.CheckSphere(worldPosition, _cellSize / 2f, _unwalkable));
                CellState state = walkable ? CellState.EMPTY : CellState.OCCUPIED;
                _grid[i, j] = new GridCell(state, worldPosition, i, j);
            }
        }
    }

    public void UpdateGrid()
    {
        Vector3 gridBottomLeft = transform.position - Vector3.right * _gridSize.x / 2 - Vector3.forward * _gridSize.y / 2;
        for (int i = 0; i < _gridSize.x; i++)
        {
            for (int j = 0; j < _gridSize.y; j++)
            {
                Vector3 worldPosition = gridBottomLeft + (Vector3.right * (_cellSize * i + _cellSize / 2)) + (Vector3.forward * (_cellSize * j + _cellSize / 2));
                bool walkable = !(Physics.CheckSphere(worldPosition, _cellSize / 2f, _unwalkable));
                CellState state = walkable ? CellState.EMPTY : CellState.OCCUPIED;
                _grid[i, j].State = state;
            }
        }
        _runtimeNavGrid.UpdateGrid(_grid);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_gridExtent.x, 1f, _gridExtent.y));
        if (_grid != null)
        {
            //GridNode playernode = WorldPointToGridNode(_player.position);
            foreach (GridCell node in _grid)
            {
                Gizmos.color = node.State == CellState.EMPTY ? Color.white : Color.red;
                if (_pathVariable.Value.Contains(node))
                {
                    Gizmos.color = Color.black;
                }
                /*                if (Path != null)
                                {
                                    if (Path.Contains(node))
                                    {
                                        Gizmos.color = Color.black;
                                    }
                                }*/
                /*                if (node == playernode)
                                {
                                    Gizmos.color = Color.green;
                                }
                */
                Gizmos.DrawCube(node.WorldPosition, new Vector3(_cellSize / 2f, _cellSize / 2f, _cellSize / 2f));
            }
        }
    }

}
