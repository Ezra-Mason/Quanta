using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : UnitHealth
{
    public override void Damage(int amount)
    {
        base.Damage(amount);
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
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
