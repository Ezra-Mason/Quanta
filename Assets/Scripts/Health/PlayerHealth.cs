using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : UnitHealth
{
    [SerializeField] private IntVariable _maxHealthVar;
    [SerializeField] private IntVariable _currentHealthVar;

    protected override void Start()
    {
        base.Start();
        _maxHealthVar.SetValue(_maxHealth);
        _currentHealthVar.SetValue(_maxHealth);
    }
    public override void Damage(int amount)
    {
        base.Damage(amount);
        _currentHealthVar.SetValue(_currentHealth);
    }

    public override void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //throw new System.NotImplementedException();
    }

    public override void Heal(int amount)
    {
        base.Heal(amount);
        _currentHealthVar.SetValue(_currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
