using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runtime Collections/Turn Action")]
public class TurnActionRuntimeCollection : RuntimeCollection<TurnAction>
{
    private void OnEnable()
    {
        _items = new List<TurnAction>();
    }

    private void OnDisable()
    {
        _items.Clear();
    }
}
