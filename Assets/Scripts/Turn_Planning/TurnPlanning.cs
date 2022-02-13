using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurnPlanning : MonoBehaviour
{
    [Header("Base Turn Planning")]
    [SerializeField] protected MovingUnit _unit;
    [SerializeField] protected IntVariable _actionToExecute;
    [SerializeField] protected RuntimeNavGrid _runtimeNavGrid;
    protected List<TurnAction> _plan;
    protected bool _hasBlocked;

    protected virtual void Start()
    {
        _plan = new List<TurnAction>();
    }

    public virtual void PrepareNextAction()
    {
        ActionType nextAction = _plan[_actionToExecute.Value].Type;
        if (nextAction == ActionType.BLOCK)
        {
            _unit.Block();
            _hasBlocked = true;
            return;
        }
        if (_hasBlocked && nextAction != ActionType.BLOCK)
        {
            _unit.Unblock();
            _hasBlocked = false;
        }

        if (nextAction == ActionType.MOVE)
        {
            Vector3 direction = new Vector3(_plan[_actionToExecute.Value].Direction.x, transform.position.y, _plan[_actionToExecute.Value].Direction.y);
            if (_runtimeNavGrid.WorldPointToGridCell(transform.position + direction).State == CellState.EMPTY)
            {
                _runtimeNavGrid.UpdateCell(transform.position + direction, CellState.MARKED, gameObject);
            }
        }
    }
    public virtual void ExecuteNextAction()
    {
        _unit.ExecuteAction(_plan[_actionToExecute.Value]);
    }

    public virtual void OnTurnStarts()
    {
        _plan.Clear();
    }
}
