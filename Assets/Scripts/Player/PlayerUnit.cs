using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MovingUnit
{
    public void ExecuteAction(TurnAction action)
    {
        switch (action.Type)
        {
            case ActionType.MOVE:
                Debug.Log(name + "Moved "+ action.Direction);
                AttemptMove<MovingUnit>(action.Direction.x, action.Direction.y);
                break;
            case ActionType.ATTACK:
                Debug.Log(name + "Attacked");
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
    }
    protected override void AttemptMove<T>(float xDirection, float zDirection)
    {
        base.AttemptMove<T>(xDirection, zDirection);
    }

    protected override void OnCantMove<T>(T Component)
    {
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
