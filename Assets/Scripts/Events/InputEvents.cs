using System;
using UnityEngine;

namespace Events
{
    public class InputEvents
    {
        public event Action<Vector2> OnMovePressed;
        public event Action OnRollPressed;
        public event Action OnMeleeAttackPressed;

        public event Action OnInteractPressed;

        public void MovePressed(Vector2 moveDir)
        {
            OnMovePressed?.Invoke(moveDir);
        }

        public void RollPressed()
        {
            OnRollPressed?.Invoke();
        }

        public void MeleePressed()
        {
            OnMeleeAttackPressed?.Invoke();
        }

        public void InteractPressed()
        {
            OnInteractPressed?.Invoke();
        }
    }
}
