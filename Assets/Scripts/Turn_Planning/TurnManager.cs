using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [Header("Navigation Grid")]
    [SerializeField] private NavGridVolume _gridVolume;
    [Header("Turn Planning Collections")]
    [SerializeField] private TurnPlanningRuntimeCollection _playerPlanning;
    [SerializeField] private TurnPlanningRuntimeCollection _enemyPlanning;
    [Header("Events")]
    [SerializeField] private GameEvent _playersTurn;
    [Header("Timings")]
    [SerializeField] private float _timeBetweenMoves = 0.2f;
    [Header("Player")]
    [SerializeField] private TurnActionRuntimeCollection _playerActions;
    [SerializeField] private IntVariable _actionToExecute;
    [SerializeField] private IntVariable _playerActionPoints;
    [SerializeField] private IntVariable _currentPlayerActionPoints;

    // Start is called before the first frame update
    void Start()
    {
        _playerActionPoints.SetValue(3);
        _currentPlayerActionPoints.SetValue(_playerActionPoints.Value);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void PlayerEndedTurn()
    {
        _actionToExecute.SetValue(0);
        Debug.Log("playerEnded actions to execute is now " + _actionToExecute.Value);
        //Start moving all the units together
        StartCoroutine(Moves());
    }
    IEnumerator Moves()
    {
        for (int i = 0; i < _playerActionPoints.Value; i++)
        {

            _gridVolume.UpdateGrid();
            _actionToExecute.SetValue(i);
            //all units prepare actions
            for (int j = 0; j < _enemyPlanning.List().Count; j++)
            {
                _enemyPlanning.List()[j].PrepareNextAction();
                _gridVolume.UpdateGrid();
            }
            for (int j = 0; j < _playerPlanning.List().Count; j++)
            {
                _playerPlanning.List()[j].PrepareNextAction();
                _gridVolume.UpdateGrid();
            }
            yield return new WaitForEndOfFrame();
            //all units now execute these actions
            for (int j = 0; j < _enemyPlanning.List().Count; j++)
            {
                _enemyPlanning.List()[j].ExecuteNextAction();
                _gridVolume.UpdateGrid();
            }
            for (int j = 0; j < _playerPlanning.List().Count; j++)
            {
                _playerPlanning.List()[j].ExecuteNextAction();
                _gridVolume.UpdateGrid();
            }
            yield return new WaitForSeconds(_timeBetweenMoves);
        }
        //once all units have moved 3 times turn the player preview on
        PlayersTurnStarted();
        yield return null;
    }
    public void PlayersTurnStarted()
    {
        _gridVolume.UpdateGrid();
        _currentPlayerActionPoints.SetValue(3);
        _playersTurn.Raise();
    }
}
