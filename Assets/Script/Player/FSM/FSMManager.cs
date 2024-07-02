using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YuLongFSM
{
    public enum FSMState
    {
        None = 0,
        Idle,
        Run,
        Walk,
        Attack,
        Hit,
        Die,
    }
    [Serializable]
    public class FSMManager<T>
    {
        public FSMState cutFSMState;
        private FSMIState<T> cutFSMIState;
        private Dictionary<FSMState, FSMIState<T>> fSMStateDic = new Dictionary<FSMState, FSMIState<T>>();
        private T fSMData;

        public FSMManager(T fSMData)
        {
            this.fSMData = fSMData;
        }
        public void Register(FSMState fSMState, FSMIState<T> fSMIState)
        {
            fSMIState.fSMData = fSMData;
            fSMIState.fSMManager = this;
            fSMStateDic.Add(fSMState, fSMIState);
        }
        public void Unregister(FSMState fSMState, FSMIState<T> fSMIState)
        {
            fSMStateDic.Add(fSMState, fSMIState);
        }

        public void Switch(FSMState fSMState)
        {
            if (!fSMStateDic.ContainsKey(fSMState))
            {
                Debug.Log("This state is not added to the current state machine");
                return;
            }
            if (cutFSMState== fSMState)
            {
                return;
            }

            FSMIState<T> fSMIState = fSMStateDic[fSMState];
            if (cutFSMIState!=null)
            {
                cutFSMIState.OnExit();
            }
            fSMIState.OnEnter();
            cutFSMIState = fSMIState;
            cutFSMState = fSMState;
        }
        public void OnUpdate()
        {
            if (cutFSMIState!=null)
            {
                cutFSMIState.OnUpdate();
            }
        }
        public void FixedUpdate()
        {
            if (cutFSMIState != null)
            {
                cutFSMIState.FixedUpdate();
            }
        }

    }
}
