using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : UnitHealth
{
    public override void Damage(int amount)
    {
        base.Damage(amount);
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
