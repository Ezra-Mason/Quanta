using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnInput : MonoBehaviour
{
    [SerializeField] private ActionControlBindings _controls;
    [SerializeField] private PlayerTurnManager _turnManager;
    [SerializeField] private IntVariable _actionPoints;
    [SerializeField] private IntVariable _currentActionPoints;
    //[SerializeField] private TurnActionRuntimeCollection _inputActions;
    [SerializeField] private GameEvent _endTurn;
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
            if (Input.GetKey(_controls.Attack))
            {
                if ((_inputDirection.x != 0f || _inputDirection.y != 0f) && _currentActionPoints.Value > 0)
                {
                    Debug.Log("Queued Attack");
                    TurnAction action = new TurnAction(ActionType.ATTACK, _inputDirection, 1);
                    //_inputActions.Add(action);
                    _turnManager.PreviewAction(action);
                    _currentActionPoints.SetValue(_currentActionPoints.Value-1);
                    _timer = _coolDownTime;
                }
            }
            else if (Input.GetKey(_controls.Block))
            {
                if ((_inputDirection.x != 0f || _inputDirection.y != 0f) && _currentActionPoints.Value > 0)
                {
                    Debug.Log("Queued Block");
                    TurnAction action = new TurnAction(ActionType.BLOCK, _inputDirection, 1);
                    //_inputActions.Add(action);
                    _turnManager.PreviewAction(action);
                    _currentActionPoints.SetValue(_currentActionPoints.Value - 1);
                    _timer = _coolDownTime;
                }
            }
            else if (Input.GetKey(_controls.Wait) && _currentActionPoints.Value > 0)
            {
                    Debug.Log("Queued Wait");
                    TurnAction action = new TurnAction(ActionType.WAIT, Vector2.zero, 1);
                    //_inputActions.Add(action);
                    _turnManager.PreviewAction(action);
                    _currentActionPoints.SetValue(_currentActionPoints.Value - 1);
                    _timer = _coolDownTime;
            }
            else if ((_inputDirection.x != 0f || _inputDirection.y != 0f) && (!Input.GetKey(_controls.Wait) && !Input.GetKey(_controls.Attack) && !Input.GetKey(_controls.Block)) && _currentActionPoints.Value > 0)
            {
                Debug.Log("Queued Move");
                TurnAction action = new TurnAction(ActionType.MOVE, _inputDirection, 1);
                //_inputActions.Add(action);
                _turnManager.PreviewAction(action);
                _currentActionPoints.SetValue(_currentActionPoints.Value - 1);
                _timer = _coolDownTime;

            }
            if (Input.GetKey(_controls.Undo))
            {
                _turnManager.UndoAction();
                _currentActionPoints.Increment();
                _timer = _coolDownTime;
            }

            if (Input.GetKeyDown(KeyCode.Return) && _currentActionPoints.Value == 0)
            {
                //end turn, execute actions 
                _endTurn.Raise();
            }
        }
    }
}
