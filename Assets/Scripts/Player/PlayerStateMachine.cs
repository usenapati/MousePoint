using Core.State_Machine;
using Player.Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerStateMachine : StateMachine<PlayerStateMachine>
    {
        [SerializeField] private PlayerInput playerInput;
        // this is the Transform we want to rotate on the Y axis when changing directions
        [SerializeField] private Transform spriteTransform;
        [SerializeField] private Rigidbody _rigidbody;

        // this Vector2 can be used on each State to determine any change
        public Vector2 Movement { get; private set; }
        public bool RollPressed => _rollPressed;
        private bool _rollPressed;

        public bool MeleeAttackPressed => _meleePressed;
        private bool _meleePressed;

        // since our sprite is facing right, we set it to true
        public bool IsFacingRight => _isFacingRight;
        private bool _isFacingRight = true;

        [SerializeField] private Animator animator;
        public PlayerAnimations Animations { get; private set; }        

        protected override void Awake()
        {
            base.Awake();
            Animations = new PlayerAnimations(animator);
        }  
        
        private void OnEnable()
        {
            playerInput.MovementEvent += HandleMove;
            playerInput.RollEvent += HandleRoll;
            playerInput.RollCancelledEvent += HandleCancelledRoll;
            playerInput.MeleeAttackEvent += HandleMelee;
        }

        private void OnDisable()
        {
            playerInput.MovementEvent -= HandleMove;
            playerInput.RollEvent -= HandleRoll;
            playerInput.RollCancelledEvent -= HandleCancelledRoll;
            playerInput.MeleeAttackEvent -= HandleMelee;
        }

        private void HandleMove(Vector2 movement)
        {
            Movement = movement;
            CheckFlipSprite(movement);
        }
        
        private void HandleRoll()
        {
            _rollPressed = true;
        }
        
        private void HandleCancelledRoll()
        {
            _rollPressed = false;
        }

        private void HandleMelee(bool isPressed)
        {
            _meleePressed = isPressed;
        }

        private void CheckFlipSprite(Vector2 velocity)
        {
            if ((!(velocity.x > 0f) || _isFacingRight) && (!(velocity.x < 0f) || !_isFacingRight)) return;
            
            _isFacingRight = !_isFacingRight;
            spriteTransform.Rotate(spriteTransform.rotation.x, -180f, spriteTransform.rotation.z);
        }

        // just  a simple implementation of movement by setting the velocity of the Rigidbody
        public void Move(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }
    }
}
