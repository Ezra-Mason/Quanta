using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] private BoolVariable _isPlayersTurn;
    [SerializeField] private BoolVariable _enemiesMoving;
    [SerializeField] private RuntimeGameObjectSet _enemies;
    [SerializeField] private GameEvent _enemiesMove;
    [SerializeField] private List<GameObject> _enemyList;
    [SerializeField] private float _turnDelay = 0.1f;
    private bool[,] _grid;

    // Start is called before the first frame update
    void Start()
    {
        _enemyList = new List<GameObject>();
        _isPlayersTurn.Value = true;
        _enemiesMoving.Value = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayersTurn.Value || _enemiesMoving.Value)
            return;
        StartCoroutine(MoveEnemies());
    }

    private IEnumerator MoveEnemies()
    {
        _enemiesMoving.Value = true;
        _enemiesMove.Raise();
        if (_enemyList.Count ==0)
        {
            yield return new WaitForSeconds(_turnDelay);
        }
        else
        {
            for (int i = 0; i < _enemies.List().Count; i++)
            {
                
            }
            yield return new WaitForSeconds(_turnDelay);

        }
        
        _enemiesMoving.Value = false;
        _isPlayersTurn.Value = true;
        Debug.Log("Enemy turn ended");
    }
}
