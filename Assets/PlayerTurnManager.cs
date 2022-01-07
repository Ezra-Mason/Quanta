using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnManager : MonoBehaviour
{
    [SerializeField] private PlayerUnit _previewPlayerUnit;
    [SerializeField] private PlayerUnit _playerUnit;
    [SerializeField] private IntVariable _actionToExecute;
    [SerializeField] private TurnActionRuntimeCollection _queuedActions;
    [Header("Events")]
    [SerializeField] private GameEvent _previewedAction;
    //[SerializeField] private List<TurnAction> _previewedActions = new List<TurnAction>();
    private TurnAction _lastAction;
    public bool IsPlanning => _isPlanning;
    [SerializeField] private bool _isPlanning;
    public bool CanInput => _canInput;
    [SerializeField] private bool _canInput;

    // Start is called before the first frame update
    void Start()
    {
        _isPlanning = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PreviewAction(TurnAction action)
    {
        _previewPlayerUnit.ExecuteAction(action);
        _queuedActions.Add(action);
        //_previewedActions.Add(action);
        _lastAction = action;
        _previewedAction.Raise();
    }
    public void ExecuteNextAction()
    {
        Debug.Log("Player Turn Manager Executing action" + _actionToExecute.Value);
        List<TurnAction> actions = _queuedActions.List();
        _playerUnit.ExecuteAction(actions[_actionToExecute.Value]);
    }

    public void UndoAction()
    {
        TurnAction reverseAction = new TurnAction(_lastAction.Type, _lastAction.Direction * -1, _lastAction.Cost);
        _previewPlayerUnit.ExecuteAction(reverseAction);
        //_previewedActions.Remove(_previewedActions[_previewedActions.Count - 1]);
        _queuedActions.Remove(_queuedActions.List()[_queuedActions.Count() - 1]);
        if (_queuedActions.Count()>0)
        {
            _lastAction = _queuedActions.List()[_queuedActions.Count() - 1];
        }
    }

    public void OnPlayersTurn()
    {
        _previewPlayerUnit.gameObject.transform.position = transform.position;
        //_previewedActions.Clear();
        _queuedActions.Clear();
        _isPlanning = true;
    }

    public void OnPlayerEndedTurn()
    {
        _isPlanning = false;
    }
}
