using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MovingUnit
{
    public override bool ExecuteAction(TurnAction action)
    {
        bool sucess = false;
        switch (action.Type)
        {
            case ActionType.MOVE:
                Debug.Log(name + "Moved " + action.Direction);
                sucess = AttemptMove<MovingUnit>(action.Direction.x, action.Direction.y);
                break;
            case ActionType.ATTACK:
                Debug.Log(name + "Attacked");
                sucess = true;
                break;
            case ActionType.BLOCK:
                Debug.Log(name + "Blocked");
                sucess = true;
                break;
            case ActionType.WAIT:
                Debug.Log(name + "Waited");
                sucess = true;
                break;
            default:
                break;
        }
        return sucess;
    }

    protected override void OnCantMove<T>(T Component)
    {
        // implement if needed
        throw new System.NotImplementedException();
    }
}
