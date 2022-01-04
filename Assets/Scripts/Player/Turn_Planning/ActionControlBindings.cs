using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Action Control Bindings")]
public class ActionControlBindings : ScriptableObject
{
    public KeyCode Attack => _attack;
    [SerializeField] private KeyCode _attack;
    public KeyCode Block => _block;
    [SerializeField] private KeyCode _block;
    public KeyCode Wait => _wait;
    [SerializeField] private KeyCode _wait;
    public KeyCode Undo => _undo;
    [SerializeField] private KeyCode _undo;
}
