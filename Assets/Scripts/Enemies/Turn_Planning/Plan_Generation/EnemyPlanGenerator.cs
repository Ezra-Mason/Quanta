using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Turn Planning/Enemy Plan Generator")]
public abstract class EnemyPlanGenerator : ScriptableObject
{
    public abstract TurnAction[] GeneratePlan(int actionPoints, Vector3 position, Vector3 targetPosition);
}
