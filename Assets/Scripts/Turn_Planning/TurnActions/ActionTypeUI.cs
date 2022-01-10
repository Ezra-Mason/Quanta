using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Action Type UI")]
public class ActionTypeUI : ScriptableObject
{
    public Sprite Empty => _empty;
    [SerializeField] private Sprite _empty;
    public Sprite Attack => _attack;
    [SerializeField] private Sprite _attack;
    public Sprite Block => _block;
    [SerializeField] private Sprite _block;
    public Sprite Move => _move;
    [SerializeField] private Sprite _move;
    public Sprite Wait => _wait;
    [SerializeField] private Sprite _wait;
}
