using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCellSelect : MonoBehaviour
{
    [SerializeField] private PlayerTurnPlanning _playerTurnPlanning;
    [SerializeField] private Transform _previewPlayer;
    [SerializeField] private IntVariable _currentActionPoints;
    [SerializeField] private RuntimeNavGrid _navGrid;
    [SerializeField] private PathVariable _path;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ActionTypeVariable _selectedAction;
    private Vector3 _target;
    public bool IsValidMousePosition => _isValidMousePosition;
    [SerializeField]
    private bool _isValidMousePosition = false;

    //pathfinding 
    private List<GridCell> openSet = new List<GridCell>();
    private HashSet<GridCell> closedSet = new HashSet<GridCell>();


    // Start is called before the first frame update
    void Start()
    {
        //GetPath(transform.position, _target.position);
    }

    // Update is called once per frame
    void Update()
    {
        //get the mouse position on the board
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, _layerMask))
        {
            _isValidMousePosition = true;
            _target = hit.point;
            Debug.DrawLine(ray.origin, _target, Color.cyan);
        }
        else
        {
            _isValidMousePosition = false;
        }
        if (_isValidMousePosition == true && (_selectedAction.Value == ActionType.MOVE || _selectedAction.Value == ActionType.ATTACK))
        {
            //find the path to the mouse position
            GetPath(_previewPlayer.position, _target);
        }
        if (_isValidMousePosition == true && _selectedAction.Value == ActionType.ATTACK)
        {
            Vector3 direction = (_target - _previewPlayer.position).normalized;
            GridCell cell = _navGrid.WorldPointToGridCell(_previewPlayer.position + direction);
            List<GridCell> temp = new List<GridCell>();
            temp.Add(cell);
            _path.Value = temp;
        }
    }

    public void GetPath(Vector3 start, Vector3 end)
    {
        //Debug.Log("Started getting path");
        GridCell startNode = _navGrid.WorldPointToGridCell(start);
        GridCell endNode = _navGrid.WorldPointToGridCell(end);
        openSet = new List<GridCell>();
        closedSet = new HashSet<GridCell>();

        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
           // Debug.Log("Openset.count >0");
            GridCell currentNode = openSet[0];
            //get the node with the lowest fcost
            for (int i = 1; i < openSet.Count; i++)
            {
                bool nodeDesireable = openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost;
                if (nodeDesireable)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            //found the end or exceeded the move limit
            if (currentNode == endNode || _currentActionPoints.Value * 8 <= GetNodeDistance(currentNode, startNode))
            {
                //Debug.Log("Found end node");
                RetracePath(startNode, currentNode);
                return;
            }

            //Debug.Log("Checking neighbours");
            //loop through the neighbouring nodes
            foreach (GridCell adj in _navGrid.GetAdjacentNodes(currentNode))
            {
                if (adj.State != CellState.EMPTY || closedSet.Contains(adj))
                    continue;

                int newConstAdj = currentNode.GCost + GetNodeDistance(currentNode, adj);
                if (newConstAdj < adj.GCost || !openSet.Contains(adj))
                {
                    adj.GCost = newConstAdj;
                    adj.HCost = GetNodeDistance(adj, endNode);
                    adj.Parent = currentNode;

                    if (!openSet.Contains(adj))
                    {
                        openSet.Add(adj);
                    }
                }
            }
        }
    }

    private int GetNodeDistance(GridCell a, GridCell b)
    {
        Vector2Int distance = new Vector2Int(Mathf.Abs(a.GridPosition.x - b.GridPosition.x),
                                            Mathf.Abs(a.GridPosition.y - b.GridPosition.y));
        if (distance.x > distance.y)
        {
            return 14 * distance.y + 10 * (distance.x - distance.y);
        }
        else
        {
            return 14 * distance.x + 10 * (distance.y - distance.x);
        }
        /*        return distance.x + distance.y;
        */
    }

    private void RetracePath(GridCell start, GridCell end)
    {
        //Debug.Log("Retracing path");
        List<GridCell> path = new List<GridCell>();
        GridCell currentNode = end;
        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        //_navGrid.Path = path;
        //_navGridVolume._path = path;
        _path.Value = path;
    }

}
