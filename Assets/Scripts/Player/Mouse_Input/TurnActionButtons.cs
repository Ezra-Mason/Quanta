using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnActionButtons : MonoBehaviour
{
    [SerializeField] private ActionTypeVariable _selectedAction;
    private ActionType _currentAction;

    // Start is called before the first frame update
    void Start()
    {
        _currentAction = _selectedAction.Value;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
