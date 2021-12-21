using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MovingObject
{
    [SerializeField] private Vector2 _nextPosition;
    [SerializeField] private EnemyAI _ai;
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
        _nextPosition = _ai.GetTargetPosition();
        base.Start();
    }


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    public void MoveEnemy()
    {
        AttemptMove<Interactable>(_nextPosition.x, _nextPosition.y);
        _nextPosition = _ai.GetTargetPosition();
    }
}
