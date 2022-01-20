using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MovingUnit
{
    [SerializeField] private PlayerGuns _guns;

    public override void ExecuteAction(TurnAction action)
    {
        switch (action.Type)
        {
            case ActionType.MOVE:
                Debug.Log(name + "Moved "+ action.Direction);
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
    }

    private void Attack(Vector2 direction)
    {
        Debug.Log(name + "Attacked in direction " +direction);
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(direction.x, 0f, direction.y);

        RaycastHit hitInfo;
        _collider.enabled = false;
        Physics.Linecast(start, end, out hitInfo, _blockingLayer);
        _collider.enabled = true;

        Collider[] colliders = Physics.OverlapSphere(end, 0.4f, _blockingLayer);

        if (colliders == null)
            return;    
        //if nothing is hit, move the object
        if (colliders[0].transform != null)
        {
            Debug.Log("hit = " + colliders[0].transform.name);
            if (colliders[0].transform.TryGetComponent<EnemyHealth>(out EnemyHealth health))
            {
                Debug.Log(name + "Damaged " + health.gameObject.name);
                health.Damage(1);
            }
        }
        if (colliders[0].transform ==null)
        {
            Debug.Log("null transform");
        }
    }
    protected override void AttemptMove<T>(float xDirection, float zDirection)
    {
        base.AttemptMove<T>(xDirection, zDirection);
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
