using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingUnit : MonoBehaviour
{
    [Header("Base Unit")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] protected MovingSettings _moveSettings;
    protected bool _moving = false;
    protected Vector3 _target;
    private float _moveTime = 0.1f;
    private float _inverseMoveTime;
    protected LayerMask _blockingLayer;
    [SerializeField] protected RuntimeNavGrid _grid;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _moveTime = _moveSettings.MoveTime;
        _blockingLayer = _moveSettings.BlockingLayer;
        _inverseMoveTime = 1f / _moveTime;
    }
    protected virtual void Update()
    {
        if (_moving)
        {
            Vector3 newPosition = Vector3.MoveTowards(_rb.position, _target, _inverseMoveTime * Time.deltaTime);
            _rb.MovePosition(newPosition);
            if ((transform.position - _target).sqrMagnitude < Mathf.Epsilon)
            {
                _moving = false;
            }
        }
    }

    /// <summary>
    /// Attempt to move in the input direction, range is defined by magnitude of input directions
    /// </summary>
    /// <param name="xDirection"></param>
    /// <param name="zDirection"></param>
    protected virtual bool AttemptMove<T>(float xDirection, float zDirection) where T : Component
    {
        bool isBlocked = IsTargetCellOccupied(xDirection, zDirection);
        // if nothing was hit, start to move to the end
        if (!isBlocked)
        {
            Vector3 end = transform.position + new Vector3(xDirection, 0f, zDirection);
            _target = end;
            _moving = true;
            return true;
        }
        return false;
    }

    protected virtual bool IsTargetCellOccupied(float xDirection, float zDirection)
    {
        Vector3 position = transform.position + new Vector3(xDirection, 0f, zDirection);
        GridCell cell = _grid.WorldPointToGridCell(position);
        if (cell.WorldPosition == transform.position)
        {
            return true;
        }

        //is the cell empty or has been marked by this moving object
        if (cell.State <= CellState.MARKED && cell.Occupier == gameObject)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected abstract void OnCantMove<T>(T Component) where T : Component;
    public abstract bool ExecuteAction(TurnAction action);

    public abstract void Block();
    public abstract void Unblock();

}
