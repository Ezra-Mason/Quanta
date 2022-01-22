using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewUnit : MovingUnit
{
    [SerializeField] private Transform _attackPreview;
/*    public override bool ExecuteAction(TurnAction action)
    {
        switch (action.Type)
        {
            case ActionType.MOVE:
                Debug.Log(name + "Moved " + action.Direction);
                AttemptMove<MovingUnit>(action.Direction.x, action.Direction.y);
                break;
            case ActionType.ATTACK:
                Attack(action.Direction);
                //_guns.Shoot(action.Direction);
                break;
            case ActionType.BLOCK:
                Debug.Log(name + "Blocked");
                break;
            case ActionType.WAIT:
                Debug.Log(name + "Waited");
                break;
            default:
                break;
        }
    }*/
    public override bool ExecuteAction(TurnAction action)
    {
        bool succeeded = false;
        switch (action.Type)
        {
            case ActionType.MOVE:
                Debug.Log(name + "Moved " + action.Direction);
                succeeded = AttemptMove<MovingUnit>(action.Direction.x, action.Direction.y);
                break;
            case ActionType.ATTACK:
                Attack(action.Direction);
                succeeded = true;
                //_guns.Shoot(action.Direction);
                break;
            case ActionType.BLOCK:
                Debug.Log(name + "Blocked");
                succeeded = true;
                break;
            case ActionType.WAIT:
                Debug.Log(name + "Waited");
                succeeded = true;
                break;
            default:
                break;
        }
        return succeeded;
    }

    private void Attack(Vector2 direction)
    {
        _attackPreview.position = transform.position + new Vector3(direction.x, 1f, direction.y);
    }

    // preview player overrides attempt move to ignore player occupied cells 
    protected override bool IsTargetCellOccupied(float xDirection, float zDirection)
    {
        Vector3 position = transform.position + new Vector3(xDirection, 0f, zDirection);
        GridCell cell = _grid.WorldPointToGridCell(position);
        if (cell.WorldPosition == transform.position)
        {
            return true;
        }
        // does the cell contain the player? ignore the player occupying if so
        Collider[] colls = Physics.OverlapSphere(position, 0.4f, _blockingLayer);
        foreach (Collider coll in colls)
        {
            if (coll.TryGetComponent<PlayerUnit>(out PlayerUnit player))
            {
                return false;
            }
        }
        // is the cell occupied or not?
        if (cell.State == CellState.EMPTY)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected override void OnCantMove<T>(T Component)
    {
        // implement if needed
        throw new System.NotImplementedException();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

}
