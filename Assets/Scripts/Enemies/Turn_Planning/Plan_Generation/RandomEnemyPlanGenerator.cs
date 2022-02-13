using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Plans/Random")]
public class RandomEnemyPlanGenerator : EnemyPlanGenerator
{
    public override List<TurnAction> GeneratePlan(int actionPoints, Vector3 position, Vector3 targetPosition)
    {
        List<TurnAction> plan = new List<TurnAction>();
        for (int i = 0; i < actionPoints; i++)
        {
            int rand = Random.Range(0, 3);
            ActionType type = (ActionType)rand;
            if (type == ActionType.ATTACK || type == ActionType.MOVE)
            {
                Vector3 dir = (targetPosition - position).normalized;
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
                plan.Add(new TurnAction(type, direction, 1));
            }
            else
            {
                plan.Add(new TurnAction(type, Vector2.zero, 1));
            }
        }
        return plan;
    }
}
