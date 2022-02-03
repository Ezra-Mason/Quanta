using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ActionType{
    MOVE,
    ATTACK,
    BLOCK,
    WAIT,
    NULL
}
[System.Serializable]
public class TurnAction 
{
    public TurnAction(ActionType type, Vector2 direction, int cost)
    {
        _direction = direction;
        _type = type;
        _cost = cost;
    }

    public Vector2 Direction => _direction;
    private Vector2 _direction;
    public ActionType Type => _type;
    private ActionType _type;
    public int Cost => _cost;
    private int _cost;

    /*   public KeyCode KeyCode { get; }
        public Vector2 Direction { get; }
        public ActionType Type { get; }
        public int Cost { get; }
    */
}
