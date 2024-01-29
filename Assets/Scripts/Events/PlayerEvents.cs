using System;

namespace Events
{
    public class PlayerEvents
    {
        public event Action OnPlayerDeath;
        public event Action OnPlayerSpawn;
        public event Action OnEnablePlayerMovement;
        public event Action OnDisablePlayerMovement;

        public void PlayerDeath()
        {
            OnPlayerDeath?.Invoke();
        }

        public void PlayerSpawn()
        {
            OnPlayerSpawn?.Invoke();
        }

        public void EnablePlayerMovement()
        {
            OnEnablePlayerMovement?.Invoke();
        }
        
        public void DisablePlayerMovement()
        {
            OnDisablePlayerMovement?.Invoke();
        }
    }
}
