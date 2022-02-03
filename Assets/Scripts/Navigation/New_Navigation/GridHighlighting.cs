using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHighlighting : MonoBehaviour
{
    [SerializeField] private PathVariable _path;
    [SerializeField] private RuntimeNavGrid _runtimeNavGrid;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private ActionTypeVariable _selectedAction;

    private List<GameObject> _highlights;
    private int _poolSize = 10;
    private int _poolIndex = 0;
    private bool _hasHidden;

    // Start is called before the first frame update
    void Start()
    {
        _highlights = new List<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject instance = Instantiate(_highlight, transform.position, Quaternion.identity, transform);
            _highlights.Add(instance);
            _highlights[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_selectedAction.Value == ActionType.MOVE ||_selectedAction.Value ==ActionType.ATTACK)
        {
            if (_hasHidden)
                _hasHidden = false;
            _poolIndex = 0;
            foreach (GridCell cell in _runtimeNavGrid.Grid)
            {
                foreach (GridCell step in _path.Value)
                {
                    if (cell.GridPosition == step.GridPosition)
                    {
                        _highlights[_poolIndex].transform.position = cell.WorldPosition;
                        _highlights[_poolIndex].SetActive(true);
                        _poolIndex++;
                    }
                }
            }
            //hide cells that arent used
            for (int i = _poolIndex; i < _highlights.Count; i++)
            {
                _highlights[i].SetActive(false);
            }
        }
        else if(!_hasHidden)
        {
            for (int i = 0; i < _highlights.Count; i++)
            {
                _highlights[i].SetActive(false);
            }
            _hasHidden = true;
        }
    }
}
