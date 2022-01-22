using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MovingUnit
{
    //[SerializeField] private PlayerGuns _guns;

    public override bool ExecuteAction(TurnAction action)
    {
        bool succeeded = false;
        switch (action.Type)
        {
            case ActionType.MOVE:
                Debug.Log(name + "Moved "+ action.Direction);
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
        Debug.Log(name + "Attacked in direction " +direction);
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(direction.x, 0f, direction.y);
        //RaycastHit hitInfo;
        //_collider.enabled = false;
        //Physics.Linecast(start, end, out hitInfo, _blockingLayer);
        //_collider.enabled = true;
        Collider[] colliders = Physics.OverlapSphere(end, 0.2f, _blockingLayer);
        if (colliders != null)
        {
            foreach (Collider coll in colliders)
            {
                if (coll.transform != null)
                {
                    Debug.Log("hit = " + coll.transform.name);
                    if (coll.transform.TryGetComponent<EnemyHealth>(out EnemyHealth health))
                    {
                        Debug.Log(name + "Damaged " + health.gameObject.name);
                        health.Damage(1);
                    }
                }
                if (coll.transform == null)
                {
                    Debug.Log("null transform");
                }
            }
        }
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
