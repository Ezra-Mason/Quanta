using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Runtime Variables/Action Type Variable")]
public class ActionTypeVariable : ScriptableObject
{
    [SerializeField] private ActionType _value;
    public ActionType Value 
    {
        get =>_value;
        set
        {
            _value = value;
        }
    }
}
