using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class RuntimeCollection<T> : ScriptableObject
{
    protected List<T> _items = new List<T>();    

    public void Add(T t)
    {
        if (!_items.Contains(t))
            _items.Add(t);
    }

    public void Remove(T t)
    {
        if (_items.Contains(t))
            _items.Remove(t);
    }

    public List<T> List()
    {
        return _items;
    }
    public int Count()
    {
        return _items.Count;
    }

    public void Clear()
    {
        _items.Clear();
    }
}
