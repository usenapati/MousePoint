using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.State_Machine
{
    public abstract class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private List<State<T>> states;
        
        [Header("DEBUG")] 
        [SerializeField] private bool debug = true;

        private State<T> _activeState;

        private T _parent;

        protected virtual void Awake()
        {
            _parent = GetComponent<T>();
        }

        protected virtual void Start()
        {
            if (states.Count <= 0) return;

            SetState(states[0]);
        }

        public void SetState(State<T> newStateType)
        {
            _activeState?.Exit();
            _activeState = newStateType;
            _activeState?.Enter(_parent);
        }

        public void SetState(Type newStateType)
        {
            var newState = states.FirstOrDefault(s => s.GetType() == newStateType);
            if (newState)
            {
                SetState(newState);
            }
        }

        protected virtual void Update()
        {
            _activeState?.Tick(Time.deltaTime);
            _activeState?.ChangeState();
        }

        /// <summary>
        /// Can be called from the Animation Timeline. This will propagate the AnimationTriggerType
        /// to the current active state.
        /// </summary>
        /// <param name="triggerType"></param>
        private void SetAnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            _activeState?.AnimationTriggerEvent(triggerType);
        }

        private void FixedUpdate()
        {
            _activeState?.FixedTick(Time.fixedDeltaTime);
        }

        private void OnGUI()
        {
            if (!debug) return;

            var content = _activeState != null ? _activeState.name : "(no active state)";
            GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        }
    }

// whatever could be called from the animation timeline
    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound,
        HitBox,
        FinishAttack,
    }
}