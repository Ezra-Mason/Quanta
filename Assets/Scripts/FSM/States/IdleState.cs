using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName ="FSM/States/Idle")]
    public class IdleState : State
    {
        public override bool EnterState()
        {
            base.EnterState();
            Debug.Log("Entered " + name);
            return true;
        }

        public override bool ExitState()
        {
            base.ExitState();
            Debug.Log("Exited " + name);
            return true;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        public override void UpdateState()
        {
            Debug.Log("Updated " + name);
        }
    }
 
}
