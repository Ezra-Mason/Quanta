using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        [SerializeField] private State _initialState;
        private State _currentState;


        private void Awake()
        {
            _currentState = null;
        }
        // Start is called before the first frame update
        void Start()
        {
            if (_initialState != null)
            {
                EnterState(_initialState);
            }
        }

        private void EnterState(State newState)
        {
            if (newState == null)
                return;
            _currentState = newState;
            _currentState.EnterState();
        }


        // Update is called once per frame
        void Update()
        {
            if (_currentState != null)
            {
                _currentState.UpdateState();
            }
        }
    }
}
