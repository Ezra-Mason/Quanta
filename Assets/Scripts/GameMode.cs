using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] private BoolVariable _isPlayersTurn;
    [SerializeField] private BoolVariable _enemiesMoving;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private float _turnDelay = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _isPlayersTurn.Value = true;
        _isPlayersTurn.Value = false;
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
        if (_enemies.Count ==0)
        {
            yield return new WaitForSeconds(_turnDelay);
        }

            yield return new WaitForSeconds(_turnDelay);
        
        _enemiesMoving.Value = false;
        _isPlayersTurn.Value = true;
        Debug.Log("Enemy turn ended");
    }
}
