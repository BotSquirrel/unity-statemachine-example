using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public abstract class StateRunner<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] List<State<T>> _states;
        private readonly Dictionary<Type, State<T>> _stateByType = new();
        private State<T> _activeState;

        protected virtual void Awake()
        {
            _states.ForEach(s => _stateByType.Add(s.GetType(), s));
            SetState(_states[0].GetType());
        }

        public void SetState(Type newState)
        {
            if (_activeState != null)
            {
                _activeState.Exit();
            }

            _activeState = _stateByType[newState];
            _activeState.Init(GetComponent<T>());
            Debug.Log(_activeState.ToString());
        }

        private void Update()
        {
            _activeState.CaptureInput();
            _activeState.Update();
            _activeState.ChangeState();   
        }

        private void FixedUpdate()
        {
            _activeState.FixedUpdate();
        }
    }
}
