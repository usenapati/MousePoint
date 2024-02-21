using System;
using Core.Managers;

namespace Health
{
    
    public class PlayerHealth : Health
    {
        public override bool Damage(float amount)
        {
            GameEventsManager.instance.playerEvents.PlayerDamaged();
            return base.Damage(amount);
        }

        // Player Input
        // Player State Machine - Sprites and Animation
        // Scene Manager/Respawn Manager - Respawn player in same scene or different scene
        protected override void Die()
        {
            GameEventsManager.instance.playerEvents.PlayerDeath();
            // Disable Player Input
            // Death Animation
            // Hide Player Sprites
            // UI Death Sequence
            // Transition to other scene if var is set (Combat zone to Lodge Scene)
            
        }

        public void Respawn()
        {
            GameEventsManager.instance.playerEvents.PlayerSpawn();
            // Show Player Sprites
            // Reset Player's Health
            // Enable Player Input
        }
    }
}

