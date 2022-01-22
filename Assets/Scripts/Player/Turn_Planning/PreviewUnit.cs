using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewUnit : MovingUnit
{
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
    }
/*    protected override bool AttemptMove<T>(float xDirection, float zDirection)
    {
        base.AttemptMove<T>(xDirection, zDirection);
    }*/

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
