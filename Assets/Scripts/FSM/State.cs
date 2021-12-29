using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public enum ExecutionState
    {
        NONE,
        ACTIVE,
        COMPLETED,
        TERMINATED
    }

    public abstract class State : ScriptableObject
    {
        public ExecutionState ExecState { get; protected set; }

        protected virtual void OnEnable()
        {
            ExecState = ExecutionState.NONE;
        }

        public virtual bool EnterState()
        {
            ExecState = ExecutionState.ACTIVE;
            return true;
        }

        public abstract void UpdateState();

        public virtual bool ExitState()
        {
            ExecState = ExecutionState.COMPLETED;
            return true;
        }
    }

}
