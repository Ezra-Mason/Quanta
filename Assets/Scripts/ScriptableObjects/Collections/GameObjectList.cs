using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Variables/GameObject List")]
public class GameObjectList : ScriptableObject
{
    public List<GameObject> Values => _values;
    [SerializeField] private List<GameObject> _values;
}
