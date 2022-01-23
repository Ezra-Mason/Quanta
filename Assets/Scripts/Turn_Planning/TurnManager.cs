using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [Header("Navigation Grid")]
    [SerializeField] private NavGridVolume _gridVolume;
    [Header("Events")]
    [SerializeField] private GameEvent _prepareEvent;
    [SerializeField] private GameEvent _executeEvent;
    [SerializeField] private GameEvent _playersTurn;
    private bool _isExecuting;
    [Header("Timings")]
    [SerializeField] private float _timeBetweenMoves = 0.2f;
    private float _timer;
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
        if (_isExecuting )//&& !_isMoving)
        {
            //StartCoroutine(Moves());
/*            _timer -= Time.deltaTime;
            if (_actionToExecute.Value < 0 && _timer<=0f)
                _isExecuting = false;

            if (_timer<=0f)
            {
                Debug.Log("Turn manager raised move, " + _actionToExecute.Value);
                _moveEvent.Raise();
                _timer = _timeBetweenMoves;
                _actionToExecute.SetValue(_actionToExecute.Value - 1);
            }*/
        }
    }

    IEnumerator Moves()
    {
        //Debug.Log("Starting Moves, ");
        for (int i = 0; i < _playerActionPoints.Value; i++)
        {

            //Debug.Log("Turn manager raised move, " + _actionToExecute.Value +"/"+ _playerActionPoints.Value);
            _gridVolume.UpdateGrid();
            _actionToExecute.SetValue(i);
            _prepareEvent.Raise(); // set invincible for blocking mark cells for moving
            yield return new WaitForEndOfFrame();
            _executeEvent.Raise(); // do the actions
            yield return new WaitForSeconds(_timeBetweenMoves);
        }
        _isExecuting = false;
        //Debug.Log("Turn manager ended moves, " + _actionToExecute.Value + "/" + _playerActionPoints.Value);

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
    public void PlayerEndedTurn()
    {
        _actionToExecute.SetValue(0);
        Debug.Log("playerEnded actions to execute is now " + _actionToExecute.Value);
        _isExecuting = true;
        _timer = _timeBetweenMoves;

        //Start moving all the units together
        //PrepareMoves();
        StartCoroutine(Moves());
    }

/*    private void PrepareMoves()
    {
        for (int i = 0; i < length; i++)
        {

        }
    }*/
}
