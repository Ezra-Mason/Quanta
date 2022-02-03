using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnPlanning : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private ActionControlBindings _controls;

    [Header("Player Data")] 
    [SerializeField] private PreviewUnit _previewPlayerUnit;
    [SerializeField] private PlayerUnit _playerUnit;
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private IntVariable _actionToExecute;
    [SerializeField] private TurnActionRuntimeCollection _queuedActions;
    public bool IsPlanning => _isPlanning;
    [SerializeField] private bool _isPlanning;

    [Header("Player Action Points")]
    [SerializeField] private IntVariable _actionPoints;
    [SerializeField] private IntVariable _currentActionPoints;

    [Header("Events")]
    [SerializeField] private GameEvent _endTurn;
    [SerializeField] private GameEvent _previewedAction;
    [SerializeField] private GameEvent _undoSelectAction;

    private bool _hasBlocked;
    private Vector3 _inputDirection;
    private TurnAction _lastAction;
    [SerializeField] private ActionTypeVariable _selectedActionType;
    private TurnAction _inputTurnAction;
    private float _timer;
    private float _coolDownTime = 0.2f;
    [Header("Navigation")]
    [SerializeField] private PlayerCellSelect _playerCellSelect;
    [SerializeField] private PathVariable _pathVariable;

    // Start is called before the first frame update
    void Start()
    {
        _isPlanning = true;
        _playerPosition.SetValue(transform.position);
        _selectedActionType.Value = ActionType.NULL;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
        }

        bool validClick = _playerCellSelect.IsValidMousePosition &&
                            Input.GetMouseButtonDown(0) && _selectedActionType.Value != ActionType.NULL;
        if (validClick)
        {
            //SelectAction(new TurnAction(_selectedActionType.Value, _inputDirection, 1));
            TurnAction pathToAction = PathToAction(_selectedActionType.Value);
            SelectAction(pathToAction);
            _timer = _coolDownTime;
            //SelectAction(new TurnAction(ActionType.MOVE, new Vector2(0f,1f), 1));
        }

        // undo the most recent selected action -> convert to button
        if (Input.GetKey(_controls.Undo) && _timer <= 0f)
        {
                UndoAction();
        }

        //end the players turn -> convert to tick button
        if (Input.GetKeyDown(KeyCode.Return) && _currentActionPoints.Value == 0 && _timer <= 0f)
        {
            //end turn, execute actions 
            _endTurn.Raise();
            _timer = _coolDownTime;
        }
    }

    private void SelectAction(TurnAction action)
    {
        if (PreviewAction(action))
        {
            _currentActionPoints.SetValue(_currentActionPoints.Value - 1);
            _inputTurnAction = action;
        }
    }

    public bool PreviewAction(TurnAction action)
    {
        if (_previewPlayerUnit.ExecuteAction(action))
        {
            _queuedActions.Add(action);
            //_previewedActions.Add(action);
            _lastAction = action;
            _previewedAction.Raise();
            return true;
        }
        return false;
    }

    public void UndoAction()
    {
        if (_currentActionPoints.Value < _actionPoints.Value)
        {

            TurnAction reverseAction = new TurnAction(_lastAction.Type, _lastAction.Direction * -1, _lastAction.Cost);
            _previewPlayerUnit.ExecuteAction(reverseAction);
            _queuedActions.Remove(_queuedActions.List()[_queuedActions.Count() - 1]);
            if (_queuedActions.Count() > 0)
            {
                _lastAction = _queuedActions.List()[_queuedActions.Count() - 1];
            }
            _undoSelectAction.Raise();
            _currentActionPoints.Increment();
        }
        _timer = _coolDownTime;
    }

    public void SetSelectedAction(ActionType type)
    {
        _selectedActionType.Value = type;
    }

    private TurnAction PathToAction(ActionType action)
    {
        if (_pathVariable.Value.Count>0)
        {
            Vector3 target = _pathVariable.Value[0].WorldPosition;
            Vector3 direction3 = target - _previewPlayerUnit.transform.position;
            Vector2 direction = new Vector2(direction3.x, direction3.z);
            return new TurnAction(action, direction, 1);
        }
        else
        {
            Vector2 direction = new Vector2(0f, 0f);
            return new TurnAction(action, direction, 1);
        }

    }
    public void OnExecuteNextAction()
    {
        //Debug.Log("Player Turn Manager Executing action" + _actionToExecute.Value);
        List<TurnAction> actions = _queuedActions.List();
        _playerUnit.ExecuteAction(actions[_actionToExecute.Value]);
    }

    public void OnPrepareNextAction()
    {
        List<TurnAction> actions = _queuedActions.List();
        if (actions[_actionToExecute.Value].Type == ActionType.BLOCK)
        {
            _playerUnit.Block();
            _hasBlocked = true;
            return;
        }
        if (_hasBlocked && actions[_actionToExecute.Value].Type != ActionType.BLOCK)
        {
            _playerUnit.Unblock();
            _hasBlocked = false;
            _playerUnit.Unblock();
        }
    }

    public void OnPlayersTurnStart()
    {
        _previewPlayerUnit.gameObject.transform.position = transform.position;
        _queuedActions.Clear();
        _isPlanning = true;
    }

    public void OnPlayerTurnEnded()
    {
        _playerPosition.SetValue(transform.position);
        _isPlanning = false;
    }
}
