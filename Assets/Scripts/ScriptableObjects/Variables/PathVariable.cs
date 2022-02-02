using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runtime Variables/NavGrid Path")]
public class PathVariable : ScriptableObject
{
    [SerializeField] private List<GridCell> _path;
    public List<GridCell> Value { get => _path; set { _path = value; } }

}
