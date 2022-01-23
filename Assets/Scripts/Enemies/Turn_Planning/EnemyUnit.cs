using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MovingUnit
{
    [Header("Enemy Unit")]
    [SerializeField] private EnemyHealth _enemyHealth;
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
                Attack(action.Direction);
                sucess = true;
                break;
            case ActionType.BLOCK:
                Block();
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


    private void Attack(Vector2 direction)
    {
        Debug.Log(name + "Attacked");
        Vector3 start = transform.position;
        Vector3 end = transform.position + new Vector3(direction.x, 0.5f, direction.y);
        Debug.Log(name + " Attacked position  " + end);

        Collider[] colliders = Physics.OverlapSphere(end, 0.4f, _blockingLayer);
        if (colliders != null)
        {
            foreach (Collider coll in colliders)
            {
                Debug.Log("hit " + coll.name);
                if (coll.transform != null)
                {
                    Debug.Log("hit = " + coll.transform.name);
                    if (coll.transform.TryGetComponent<PlayerHealth>(out PlayerHealth health))
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
        else
        {
            Debug.Log("missed attack ");
        }

    }

    private void Block()
    {
        Debug.Log(name + "Blocked");
        _enemyHealth.SetInvincible(true);
    }

    public void Unblock()
    {
        _enemyHealth.SetInvincible(false);
    }

    protected override void OnCantMove<T>(T Component)
    {
        // implement if needed
        throw new System.NotImplementedException();
    }
}
