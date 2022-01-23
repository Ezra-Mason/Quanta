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
    public Sprite MoveUp => _moveUp;
    [SerializeField] private Sprite _moveUp;
    public Sprite MoveDown => _moveDown;
    [SerializeField] private Sprite _moveDown;
    public Sprite MoveLeft => _moveLeft;
    [SerializeField] private Sprite _moveLeft;
    public Sprite MoveRight => _moveRight;
    [SerializeField] private Sprite _moveRight;
    public Sprite Wait => _wait;
    [SerializeField] private Sprite _wait;
}
