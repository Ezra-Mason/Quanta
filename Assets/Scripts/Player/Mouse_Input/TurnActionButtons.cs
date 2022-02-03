using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnActionButtons : MonoBehaviour
{
    [SerializeField] private ActionTypeVariable _selectedAction;
    [SerializeField] private IntVariable _currentActionPoints;
    [SerializeField] private RectTransform _moveUI;
    private Vector3 _movePos;
    [SerializeField] private RectTransform _attackUI;
    private Vector3 _attackPos;
    [SerializeField] private GameEvent _playerEndTurn;
    [SerializeField] private GameEvent _playerUndo;
    private ActionType _currentAction;

    // Start is called before the first frame update
    void Start()
    {
        _currentAction = _selectedAction.Value;
        _movePos = _moveUI.position;
        _attackPos = _attackUI.position;
    }

    private void SelectAction(ActionType actionType)
    {
        if (_selectedAction.Value == actionType)
        {
            if (actionType == ActionType.MOVE)
            {
                _moveUI.position = _movePos;
            }
            if (actionType == ActionType.ATTACK)
            {
                _attackUI.position = _attackPos;
            }
            _selectedAction.Value = ActionType.NULL;
        }
        else
        {
            if (actionType == ActionType.MOVE)
            {
                _moveUI.position = new Vector3(_movePos.x, _movePos.y + 50f, _movePos.z);
            }
            if (actionType == ActionType.ATTACK)
            {
                _attackUI.position = new Vector3(_attackPos.x, _attackPos.y + 50f, _attackPos.z);
            }
            _selectedAction.Value = actionType;
        }
    }

    private void Update()
    {
        //disable do button if not all actions assigned
        //disable undo button if there are no more actions to undo
    }
    public void OnSelectedMove()
    {
        SelectAction(ActionType.MOVE);
    }
    public void OnSelectedAttack()
    {
        SelectAction(ActionType.ATTACK);
    }
    public void OnSelectedBlock()
    {
        SelectAction(ActionType.BLOCK);
    }
    public void OnSelectedWait()
    {
        SelectAction(ActionType.WAIT);
    }

    public void OnGoSelected()
    {
        if(_currentActionPoints.Value == 0)
        {
            _playerEndTurn.Raise();
        }
    }

    public void OnUndoSelected()
    {
        _playerUndo.Raise();
    }
}
