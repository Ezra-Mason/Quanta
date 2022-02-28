using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private RuntimeNavGrid _runtimeNavGrid;
    private List<GridCell> _gridCells;
    [Header("Objects to Lay out")]
    [SerializeField] private GameObject[] _props;
    [SerializeField] private Range _propRange;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        LayoutBoard();
    }
    private void Init()
    {
        _gridCells = new List<GridCell>();
        foreach (GridCell cell in _runtimeNavGrid.Grid)
        {
            if (cell.State ==CellState.EMPTY)
            {
                _gridCells.Add(cell);
            }
        }
        Debug.Log("Initialised room");
    }

    public void LayoutBoard()
    {
        LayoutObjectsAtRandom(_props, _propRange.Minimum, _propRange.Maximum);
    }

    void LayoutObjectsAtRandom(GameObject[] possibleObjects, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1); // how many we will spawn
        for (int i = 0; i < objectCount; i++)
        {
            int cellChoice = Random.Range(0, _gridCells.Count);
            Vector3 randomPosition = _gridCells[cellChoice].WorldPosition;
            GameObject choice = possibleObjects[Random.Range(0, possibleObjects.Length)];
            Instantiate(choice, randomPosition, Quaternion.identity);
            _gridCells.RemoveAt(cellChoice);
            //tileChoice.transform.SetParent(transform);
        }
        Debug.Log("Layout object");
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
