using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private NavGrid _navGrid;
    [SerializeField] private Transform _seeker;
    //[SerializeField] private Transform _target;
    public List<GridNode> Path => _path;
    private List<GridNode> _path;


    private void Update()
    {
        //GetPath(_seeker.position, _target.position);
    }
    public void GetPath(Vector3 start, Vector3 end)
    {
        GridNode startNode = _navGrid.WorldPointToGridNode(start);
        GridNode endNode = _navGrid.WorldPointToGridNode(end);
        List<GridNode> openSet = new List<GridNode>();
        HashSet<GridNode> closedSet = new HashSet<GridNode>();

        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            GridNode currentNode = openSet[0];
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

            //found the end
            if (currentNode == endNode)
            {
                //Debug.Log("Found end node");
                RetracePath(startNode, endNode);
                return;
            }

            //loop through the neighbouring nodes
            foreach (GridNode adj in _navGrid.GetAdjacentNodes(currentNode))
            {
                if (!adj.Walkable || closedSet.Contains(adj))
                {
                    continue;
                }

                int newConstAdj = currentNode.GCost + GetNodeDistance(currentNode, adj);
                if (newConstAdj <adj.GCost || !openSet.Contains(adj))
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


    private int GetNodeDistance(GridNode a, GridNode b)
    {
        Vector2Int distance = new Vector2Int(Mathf.Abs(a.GridPosition.x - b.GridPosition.x), 
                                            Mathf.Abs(a.GridPosition.y - b.GridPosition.y));
        if (distance.x>distance.y)
        {
            return 14 * distance.y + 10 * (distance.x - distance.y);
        }
        else
        {
            return 14 * distance.x + 10 * (distance.y - distance.x);
        }
/*        return distance.x + distance.y;
*/    }


    private void RetracePath(GridNode start, GridNode end)
    {
        //Debug.Log("retracing path");
        List<GridNode> path = new List<GridNode>();
        GridNode currentNode = end;
        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();

        _navGrid.Path = path;
        _path = path;
    }
}
