using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTurnPlanning : MonoBehaviour
{
    [SerializeField] private EnemyUnit _unit;
    [SerializeField] private EnemyPlanGenerator _planGenerator;
    private TurnAction[] _plan;
    private ActionType[] _plannedTypes;
    private int _actionPoints = 3;
    [SerializeField] private SpriteRenderer[] _images;
    [SerializeField] private ActionTypeUI _ui;
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private IntVariable _actionToExecute;
    private bool _hasBlocked;


    // Start is called before the first frame update
    void Start()
    {
        _plannedTypes = new ActionType[_actionPoints];
        _plan = new TurnAction[_actionPoints];
        GeneratePlan();
    }

    public void OnMoveEvent()
    {
        _unit.ExecuteAction(_plan[_actionToExecute.Value]);
        if (_plan[_actionToExecute.Value].Type == ActionType.BLOCK)
        {
            _hasBlocked = true;
            return;
        }
        if (_hasBlocked && _plan[_actionToExecute.Value].Type != ActionType.BLOCK)
        {
            _hasBlocked = false;
            _unit.Unblock();
        }
    }

    public void GeneratePlan()
    {
        _plan = _planGenerator.GeneratePlan(_actionPoints, transform.position, _playerPosition.Value);
        for (int i = 0; i < _actionPoints; i++)
        {
            ShowImage(_plan[i], i);
        }
        Debug.Log(" Enemy plan = " + _plan[0].Type + "->" + _plan[1].Type + "->" + _plan[2].Type);
    }

    private void ShowImage(TurnAction action, int image)
    {
        switch (action.Type)
        {
            case ActionType.MOVE:
                if (action.Direction.x == 1)
                {
                    _images[image].sprite = _ui.MoveRight;
                }
                if (action.Direction.x == -1)
                {
                    _images[image].sprite = _ui.MoveLeft;
                }
                if (action.Direction.y == 1)
                {
                    _images[image].sprite = _ui.MoveUp;
                }
                if (action.Direction.y == -1)
                {
                    _images[image].sprite = _ui.MoveDown;
                }
                //_images[image].sprite = _ui.Move;
                break;
            case ActionType.ATTACK:
                _images[image].sprite = _ui.Attack;
                break;
            case ActionType.BLOCK:
                _images[image].sprite = _ui.Block;
                break;
            case ActionType.WAIT:
                _images[image].sprite = _ui.Wait;
                break;
            default:
                _images[image].sprite = _ui.Empty;
                break;
        }
    }
}
