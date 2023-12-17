using UnityEngine;

namespace Core.State_Machine
{
    public abstract class State<T> : ScriptableObject where T : MonoBehaviour
    {
        protected T runner;

        // called whenever we enter this state. Good for setting up variables
        public virtual void Enter(T parent)
        {
            runner = parent;
        }

        // similar to Update
        public abstract void Tick(float deltaTime);

        // similar to FixedUpdate
        public abstract void FixedTick(float fixedDeltaTime);

        // here we put the conditions to change to another state if needed
        public abstract void ChangeState();

        // this one can be called from the animation timeline
        public virtual void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
        }

        public virtual void Exit()
        {
        }
    }
}

