using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

[RequireComponent(typeof(Pathfinding))]
public class AIController : MonoBehaviour
{
    [SerializeField] private Pathfinding _pathfinding;
    [SerializeField] private FiniteStateMachine _fsm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
