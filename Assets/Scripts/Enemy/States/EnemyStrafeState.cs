using Core.State_Machine;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Strafe")]
    public class EnemyStrafeState : State<EnemyStateMachine>
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public override void Tick(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeState()
        {
            throw new System.NotImplementedException();
        }
    }
}
