using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private RoomData _roomData;
    private GameObject[] _roomPrefabs;
    private List<GameObject> _rooms = new List<GameObject>();

    [SerializeField] private int _currentRoom=0;
    [SerializeField] private IntVariable _currentLevel;
    [SerializeField] private IntVariable _maxLevels;
    [SerializeField] private GameEvent _resetPlayer;
    [SerializeField] private GameEvent _loadingRooms;
    [SerializeField] private GameEvent _loadComplete;
    [SerializeField] private float _goalWaitTime;
    [SerializeField] private float _loadingWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        _roomPrefabs = _roomData.Rooms;
        for (int i = 0; i < _roomPrefabs.Length; i++)
        {
            GameObject room = Instantiate(_roomPrefabs[i]);
            _rooms.Add(room);
            room.SetActive(false);
        }
        _currentRoom = 0;
        _rooms[_currentRoom].SetActive(true);
        _maxLevels.SetValue(_rooms.Count);
        _loadComplete.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentLevel.Value!=_currentRoom +1)
        {
            _currentLevel.SetValue(_currentRoom+1);
        }
    }


    public void StartLoadNextRoom()
    {
        Debug.Log("Started loading");
        StartCoroutine(WaitToLoad());
    }
    IEnumerator WaitToLoad()
    {
        Debug.Log("Waiting at goal");
        yield return new WaitForSeconds(_goalWaitTime);
        Debug.Log("Loading");
        _loadingRooms.Raise();
        yield return new WaitForSeconds(0.5f);
        LoadNextRoom();
    }


    private void LoadNextRoom()
    {
        _rooms[_currentRoom].SetActive(false);
        if (_currentRoom==_rooms.Count-1)
        {
            Debug.Log("Current room=  "+_currentRoom +"Loading End screen");
            LoadEndScreen();//_currentRoom = 0;
        }
        else
        {
            _currentRoom++;
        }
        _rooms[_currentRoom].SetActive(true);
        StartCoroutine(WaitToStart());
    }

    private void LoadEndScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator WaitToStart()
    {

        _resetPlayer.Raise();
        yield return new WaitForSeconds(_loadingWaitTime);
        _loadComplete.Raise();
    }
}
