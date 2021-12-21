using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Pathfinding _pathfinding;
    [SerializeField] private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 GetTargetPosition()
    {
        _pathfinding.GetPath(transform.position, _target.position);
        Vector3 temp = _pathfinding.Path == null ? _target.position : _pathfinding.Path[0].WorldPosition;
        Debug.Log(temp +", "+ transform.position);
        Vector3 dir = (temp - transform.position).normalized;
        Vector2 direction = new Vector2(dir.x, dir.z);
        if (Mathf.Abs(direction.x) != 0)
        {
            direction.x = Mathf.Sign(direction.x) * 1;
            direction.y = 0f;
        }
        if (Mathf.Abs(direction.y) != 0)
        {
            direction.y = Mathf.Sign(direction.y) * 1;
            direction.x = 0f;
        }
        Debug.Log(direction);
        return direction;
        //return new Vector2((int)Random.Range(-1, 1), (int)Random.Range(-1, 1));
    }
}
