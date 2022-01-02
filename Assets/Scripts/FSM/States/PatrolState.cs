using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/States/Patrol", order = 2)]
    public class PatrolState : State
    {
        public override bool EnterState()
        {
            return base.EnterState();
        }

        public override bool ExitState()
        {
            return base.ExitState();
        }

        public override void UpdateState()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
