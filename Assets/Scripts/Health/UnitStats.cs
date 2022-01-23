using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Units/UnitStats")]
public class UnitStats : ScriptableObject
{
    public int MaxHealth => _maxHealth;
    [SerializeField] private int _maxHealth;
}
