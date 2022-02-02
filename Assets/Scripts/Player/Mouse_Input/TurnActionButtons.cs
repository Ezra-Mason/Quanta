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

    public void SelectAction(ActionType actionType)
    {
        _selectedAction.Value = actionType;
    }
}
