using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingUnit : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private BoxCollider _collider;
    [SerializeField] protected MovingSettings _moveSettings;
    protected bool _moving = false;
    private Vector3 _target;
    private float _moveTime = 0.1f;
    private float _inverseMoveTime;
    private LayerMask _blockingLayer;


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
    /// Attempt to move/interact in the direction stated, range is defined by magnitude of input directions
    /// </summary>
    /// <param name="xDirection"></param>
    /// <param name="zDirection"></param>
    protected virtual void AttemptMove<T>(float xDirection, float zDirection) where T : Component
    {
        RaycastHit hit;
        bool canMove = CheckMove(xDirection, zDirection, out hit);
        // if nothing was hit, start to move to the end
        if (canMove && hit.transform == null)
        {
            Vector3 end = transform.position + new Vector3(xDirection, 0f, zDirection);
            _target = end;
            _moving = true;
            //StartCoroutine(SmoothMove(end));
            return;
        }

        //if a generic component was found and was hit, call the on move function
        //(to be implemented in inheriting class)
/*        T hitComponent;
        hit.transform.TryGetComponent<T>(out hitComponent);
        if (!canMove && hit.transform != null)
            OnCantMove(hitComponent);
*/
    }

    protected bool CheckMove(float xDirection, float zDirection, out RaycastHit hit)
    {
        Vector3 start = transform.position;
        Vector3 end = start + new Vector3(xDirection, 0f, zDirection);

        RaycastHit hitInfo;
        _collider.enabled = false;
        Physics.Linecast(start, end, out hitInfo, _blockingLayer);
        _collider.enabled = true;
        hit = hitInfo;

        //if nothing is hit, move the object
        if (hitInfo.transform == null)
        {
            return true;
        }
        return false;
    }
    protected abstract void OnCantMove<T>(T Component) where T : Component;
    public abstract void ExecuteAction(TurnAction action);
}
