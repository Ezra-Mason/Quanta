using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Moving Objects/Moving Settings")]
public class MovingSettings : ScriptableObject
{
    public float MoveTime => _moveTime;
    [SerializeField] private float _moveTime;
    public LayerMask BlockingLayer => _blockingLayer;
    [SerializeField] private LayerMask _blockingLayer;

}
