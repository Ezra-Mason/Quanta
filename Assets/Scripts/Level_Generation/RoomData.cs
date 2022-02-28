using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="RoomData")]
public class RoomData : ScriptableObject
{
    public GameObject[] Rooms => _rooms;
    [SerializeField] private GameObject[] _rooms;
}
