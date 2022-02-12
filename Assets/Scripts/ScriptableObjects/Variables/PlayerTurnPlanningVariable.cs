using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Runtime Variables/PlayerTurnPlanning")]
public class PlayerTurnPlanningVariable : ScriptableObject
{
    [SerializeField] private PlayerTurnPlanning _value;
    public PlayerTurnPlanning Value => _value;
    public void SetValue(PlayerTurnPlanning newValue)
    {
        _value = newValue;
    }
}
