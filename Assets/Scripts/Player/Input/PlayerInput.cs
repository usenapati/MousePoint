using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class PlayerInput : MonoBehaviour, GameControls.IPlayerActions, GameControls.ICoreActions
    {
        public event UnityAction<Vector2> MovementEvent = delegate { };
        public event UnityAction RollEvent = delegate {  };
        public event UnityAction RollCancelledEvent = delegate { };
        public event UnityAction<bool> MeleeAttackEvent = delegate { };
        public event UnityAction<bool> InteractEvent = delegate { };
        public event UnityAction<bool> ExitEvent = delegate { };

        private GameControls _playerActions;
        private GameControls.ICoreActions _coreActionsImplementation;

        private void OnEnable()
        {
            if (_playerActions == null)
            {
                _playerActions = new GameControls();
                _playerActions.Player.SetCallbacks(this);
                _playerActions.Core.SetCallbacks(this);
            }

            EnableGameplayInput();
        }

        private void OnDisable()
        {
            DisableAllInput();
        }

        public void DisableAllInput()
        {
            _playerActions.Player.Disable();
            _playerActions.Core.Disable();
        }

        public void EnableGameplayInput()
        {
            if (_playerActions.Player.enabled) return;

            // _playerActions.UI.Disable();
            _playerActions.Core.Enable();
            _playerActions.Player.Enable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            MovementEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                RollEvent?.Invoke();
            } 
            if (context.canceled)
            {
                RollCancelledEvent?.Invoke();
            }
        }

        public void OnMelee(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                MeleeAttackEvent?.Invoke(true);
            }
            else if (context.canceled)
            {
                MeleeAttackEvent?.Invoke(false);
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                InteractEvent?.Invoke(true);
            }
            else if (context.canceled)
            {
                InteractEvent?.Invoke(false);
            }
        }

        public void OnExit(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                ExitEvent?.Invoke(true);
            }
            else if (context.canceled)
            {
                ExitEvent?.Invoke(false);
            }
        }
    }
}
