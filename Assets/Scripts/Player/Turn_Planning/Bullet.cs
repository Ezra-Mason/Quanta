using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MovingUnit
{
    private TurnAction _action;
    public void Spawn(Vector2 direction)
    {
        _action = new TurnAction(ActionType.MOVE, direction, 1);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Move()
    {
        ExecuteAction(_action);
    }
    public override bool ExecuteAction(TurnAction action)
    {
        if (action.Type == ActionType.MOVE)
        {
            return AttemptMove<MovingUnit>(action.Direction.x, action.Direction.y);
        }

        return false;
    }

    protected override void OnCantMove<T>(T Component)
    {
        throw new System.NotImplementedException();
    }

}
