using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Navigation/Runtime NavGrid")]
public class RuntimeNavGrid : ScriptableObject
{
    public GridCell[,] Grid => _grid;
    [SerializeField] private GridCell[,] _grid;
    private Vector2Int _gridExtent = new Vector2Int(10, 10);
    private Vector2Int _gridSize;


    // Start is called before the first frame update
    private void OnEnable()
    {
    }

    public void SetUpGrid(GridCell[,] grid, Vector2Int extent, Vector2Int size)
    {
        _grid = grid;
        _gridExtent = extent;
        _gridSize = size;
    }

    public void UpdateGrid(GridCell[,] grid)
    {
        _grid = grid;
    }


    public GridCell WorldPointToGridCell(Vector3 position)
    {
        float x = Mathf.Clamp01((position.x + _gridExtent.x / 2) / _gridExtent.x);
        float y = Mathf.Clamp01((position.z + _gridExtent.y / 2) / _gridExtent.y);

        int newX = Mathf.RoundToInt((_gridSize.x - 1) * x);
        int newY = Mathf.RoundToInt((_gridSize.y - 1) * y);

        return _grid[newX, newY];
    }


    public List<GridCell> GetAdjacentNodes(GridCell node)
    {
        List<GridCell> adjacent = new List<GridCell>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0 || Mathf.Abs(i) + Mathf.Abs(j) == 2)
                    continue;
                Vector2Int check = new Vector2Int(node.GridPosition.x + i, node.GridPosition.y + j);
                bool insideGrid = check.x >= 0 && check.x < _gridSize.x && check.y >= 0 && check.y < _gridSize.y;
                if (insideGrid)
                {
                    adjacent.Add(_grid[check.x, check.y]);
                }
            }
        }
        return adjacent;
    }

}
