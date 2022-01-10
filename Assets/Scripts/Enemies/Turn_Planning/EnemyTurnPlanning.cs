using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTurnPlanning : MonoBehaviour
{
    [SerializeField] private EnemyUnit _unit;
    private TurnAction[] _plan;
    private ActionType[] _plannedTypes;
    private int _actionPoints = 3;
    [SerializeField] private SpriteRenderer[] _images;
    [SerializeField] private ActionTypeUI _ui;
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private IntVariable _actionToExecute;


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
        
    }

    public void GeneratePlan()
    {
        for (int i = 0; i < _actionPoints; i++)
        {
            int rand = Random.Range(0, 3);
            ActionType type = (ActionType)rand;
            ShowImage(type, i);
            _plannedTypes[i] = type;
            if (type == ActionType.ATTACK || type == ActionType.MOVE)
            {
                Vector3 dir = (_playerPosition.Value - transform.position).normalized;
                Vector2 direction = new Vector2(dir.x, dir.z);
                if (Mathf.Abs(direction.x) != 0)
                {
                    direction.x = Mathf.Sign(direction.x) * 1;
                    direction.y = 0f;
                }
                if (Mathf.Abs(direction.y) != 0)
                {
                    direction.y = Mathf.Sign(direction.y) * 1;
                    direction.x = 0f;
                }
                _plan[i] = new TurnAction(type, direction, 1);
            }
            else
            {
                _plan[i] = new TurnAction(type, Vector2.zero, 1);
            }
        }
    }

    private void ShowImage(ActionType type, int image)
    {
        switch (type)
        {
            case ActionType.MOVE:
                _images[image].sprite = _ui.Move;
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
