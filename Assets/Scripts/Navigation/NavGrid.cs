using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridExtent = new Vector2Int(10, 10);
    [SerializeField] private float _nodeSize = 1f;
    [SerializeField] private LayerMask _unwalkable;
    private GridNode[,] _grid;
    private Vector2Int _gridSize;
    [SerializeField] private Transform _player;
    

    // Start is called before the first frame update
    void Awake()
    {
        _gridSize = new Vector2Int(Mathf.RoundToInt(_gridExtent.x / _nodeSize), Mathf.RoundToInt(_gridExtent.y / _nodeSize));
        CreateGrid();
    }

    private void CreateGrid()
    {
        _grid = new GridNode[_gridSize.x, _gridSize.y];
        Vector3 gridBottomLeft = transform.position - Vector3.right * _gridSize.x / 2 - Vector3.forward * _gridSize.y/2;

        for (int i = 0; i < _gridSize.x; i++)
        {
            for (int j = 0; j < _gridSize.y; j++)
            {
                Vector3 worldPosition = gridBottomLeft + (Vector3.right * (_nodeSize * i +_nodeSize/2)) + (Vector3.forward * (_nodeSize * j + _nodeSize / 2));
                bool walkable = !(Physics.CheckSphere(worldPosition, _nodeSize / 2f, _unwalkable));
                _grid[i, j] = new GridNode(walkable, worldPosition, i, j);
            }
        }
    }

    public GridNode WorldPointToGridNode(Vector3 position)
    {
        float x = Mathf.Clamp01((position.x + _gridExtent.x / 2) / _gridExtent.x);
        float y = Mathf.Clamp01((position.z + _gridExtent.y / 2) / _gridExtent.y);

        int newX = Mathf.RoundToInt((_gridSize.x - 1) * x);
        int newY = Mathf.RoundToInt((_gridSize.y - 1) * y);

        return _grid[newX, newY];
    }

    public List<GridNode> GetAdjacentNodes(GridNode node)
    {
        List<GridNode> adjacent = new List<GridNode>();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0 || Mathf.Abs(i)+Mathf.Abs(j) ==2)
                    continue;
                Vector2Int check = new Vector2Int(node.GridPosition.x + i, node.GridPosition.y + j);
                bool insideGrid = check.x >= 0 && check.x < _gridSize.x && check.y >= 0 && check.y < _gridSize.y;
                if (insideGrid)
                {
                    adjacent.Add(_grid[check.x, check.y]);
                }
            }
        }

        /*        List<Vector2Int> check = new List<Vector2Int>();
                check.Add(new Vector2Int(node.GridPosition.x + 1, node.GridPosition.y));
                check.Add(new Vector2Int(node.GridPosition.x - 1, node.GridPosition.y));
                check.Add(new Vector2Int(node.GridPosition.x, node.GridPosition.y + 1));
                check.Add(new Vector2Int(node.GridPosition.x, node.GridPosition.y - 1));
                foreach ( Vector2Int adj in check)
                {
                    bool insideGrid = adj.x >= 0 && adj.x < _gridSize.x && adj.y >= 0 && adj.y < _gridSize.y;
                    if (insideGrid)
                    {
                        adjacent.Add(_grid[adj.x, adj.y]);
                    }

                }*/

        return adjacent;
    }

    public List<GridNode> Path;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(_gridExtent.x, 1f, _gridExtent.y));
        if (_grid!=null)
        {
            GridNode playernode = WorldPointToGridNode(_player.position);
            foreach (GridNode node in _grid)
            {
                Gizmos.color = node.Walkable ? Color.white : Color.red;

                if (Path !=null)
                {
                    if (Path.Contains(node))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                if (node == playernode)
                {
                    Gizmos.color = Color.green;
                }
                Gizmos.DrawCube(node.WorldPosition, new Vector3(_nodeSize / 2f, _nodeSize / 2f, _nodeSize / 2f));
            }
        }
    }

}
