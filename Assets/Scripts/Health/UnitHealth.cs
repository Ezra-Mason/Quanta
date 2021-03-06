using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitHealth : MonoBehaviour
{
    [SerializeField] private UnitStats _stats;
    protected int _maxHealth;
    protected int _currentHealth;
    protected bool _isInvicible;


    protected virtual void Start()
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

        if (_isInvicible)
            return;
        
        _currentHealth -= amount;
        if (_currentHealth <=0)
        {
            Die();
        }
    }

    public void SetInvincible(bool isInvicible)
    {
        _isInvicible = isInvicible;
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
