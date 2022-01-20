using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitHealth : MonoBehaviour
{
    [SerializeField] private UnitStats _stats;
    private int _maxHealth;
    private int _currentHealth;


    protected void Start()
    {
        _maxHealth = _stats.MaxHealth;
        _currentHealth = _maxHealth;
    }
    public virtual void Damage(int amount)
    {
        if (amount <= 0)
        {
            Debug.Log("Invalid damage amount");
            return;
        }

        _currentHealth -= amount;
        if (_currentHealth <=0)
        {
            Die();
        }
    }

    public abstract void Die();

    public virtual void Heal(int amount)
    {
        if (amount <= 0)
        {
            Debug.Log("Invalid Heal amount");
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
    }
}
