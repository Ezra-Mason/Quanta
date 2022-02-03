using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnActionButtons : MonoBehaviour
{
    [SerializeField] private ActionTypeVariable _selectedAction;
    [SerializeField] private IntVariable _currentActionPoints;
    [SerializeField] private GameEvent _playerEndTurn;
    [SerializeField] private GameEvent _playerUndo;
    private ActionType _currentAction;

    // Start is called before the first frame update
    void Start()
    {
        _currentAction = _selectedAction.Value;
    }

    private void SelectAction(ActionType actionType)
    {
        if (_selectedAction.Value == actionType)
        {
            _selectedAction.Value = ActionType.NULL;
        }
        else
        {
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
