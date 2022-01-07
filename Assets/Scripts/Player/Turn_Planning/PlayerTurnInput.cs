using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnInput : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private PlayerTurnManager _turnManager;
    [SerializeField] private ActionControlBindings _controls;
    [Header("Player Action Points")]
    [SerializeField] private IntVariable _actionPoints;
    [SerializeField] private IntVariable _currentActionPoints;
    //[SerializeField] private TurnActionRuntimeCollection _inputActions;
    [Header("Events")]
    [SerializeField] private GameEvent _endTurn;

    public ActionType SelectedAction => _selectedAction;
    [SerializeField] private ActionType _selectedAction;

    private float _timer;
    private float _coolDownTime = 0.2f;
    private Vector2 _inputDirection = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer>0f)
        {
            _timer -= Time.deltaTime;
        }

        if (_turnManager.IsPlanning && _timer<=0f)
        {
            _inputDirection.x = (int)Input.GetAxisRaw("Horizontal");
            _inputDirection.y = (int)Input.GetAxisRaw("Vertical");
            //make diagonals verticals
            if (_inputDirection.x != 0f)
                _inputDirection.y = 0f;

            if (Input.GetKeyDown(_controls.Attack))
            {
                if (_selectedAction == ActionType.MOVE)
                {
                    Debug.Log("Player selected attack");
                    _selectedAction = ActionType.ATTACK;
                }
                else if (_selectedAction == ActionType.ATTACK)
                {
                    Debug.Log("Player deselected attack");
                    _selectedAction = ActionType.MOVE;
                }
            }
            else if (Input.GetKeyDown(_controls.Block))
            {
                if (_selectedAction == ActionType.MOVE)
                {
                    Debug.Log("Player selected block");
                    _selectedAction = ActionType.BLOCK;
                }
                else if (_selectedAction == ActionType.BLOCK)
                {
                    Debug.Log("Player deselected block");
                    _selectedAction = ActionType.MOVE;
                }

            }
            else if (Input.GetKeyDown(_controls.Wait))
            {
                if (_selectedAction == ActionType.MOVE)
                {
                    Debug.Log("Player selected wait");
                    _selectedAction = ActionType.WAIT;
                }
                else if (_selectedAction == ActionType.WAIT)
                {
                    Debug.Log("Player deselected wait");
                    _selectedAction = ActionType.MOVE;
                }

            }

            bool directionalInput = (_inputDirection.x != 0f || _inputDirection.y != 0f) && _currentActionPoints.Value > 0;
            switch (_selectedAction)
            {
                case ActionType.MOVE:
                    if (directionalInput)
                    {
                        Debug.Log("Queued Move");
                        SelectAction(new TurnAction(ActionType.MOVE, _inputDirection, 1));
                    }
                    break;
                case ActionType.ATTACK:
                    if (directionalInput)
                    {
                        Debug.Log("Queued Attack");
                       SelectAction(new TurnAction(ActionType.ATTACK, _inputDirection, 1));
                    }
                    break;
                case ActionType.BLOCK:
                    Debug.Log("Queued Block");
                    SelectAction(new TurnAction(ActionType.BLOCK, _inputDirection, 1));
                    break;
                case ActionType.WAIT:
                    Debug.Log("Queued Wait");
                    SelectAction(new TurnAction(ActionType.WAIT, Vector2.zero, 1));
                    break;
                default:
                    break;
            }
            
            // undo the most recent selected action
            if (Input.GetKey(_controls.Undo))
            {
                _turnManager.UndoAction();
                _currentActionPoints.Increment();
                _timer = _coolDownTime;
            }

            //end the players turn
            if (Input.GetKeyDown(KeyCode.Return) && _currentActionPoints.Value == 0)
            {
                //end turn, execute actions 
                _endTurn.Raise();
            }
        }

        
    }
    private void SelectAction(TurnAction action)
    {
        _turnManager.PreviewAction(action);
        _currentActionPoints.SetValue(_currentActionPoints.Value - 1);
        _timer = _coolDownTime;
        _selectedAction = ActionType.MOVE;
    }
}
