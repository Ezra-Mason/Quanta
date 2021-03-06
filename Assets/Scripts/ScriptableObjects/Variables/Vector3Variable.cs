using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runtime Variables/Vector3")]
public class Vector3Variable : ScriptableObject
{
    [SerializeField] private Vector3 _value;
    public Vector3 Value => _value;

    public void SetValue(Vector3 newValue)
    {
        _value = newValue;
    }

}
