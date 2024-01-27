namespace Health
{
    
    public class PlayerHealth : Health
    {
        // Player Input
        // Player State Machine - Sprites and Animation
        // Scene Manager/Respawn Manager - Respawn player in same scene or different scene
        protected override void Die()
        {
            // Disable Player Input
            // Death Animation
            // Hide Player Sprites
            // UI Death Sequence
            // Transition to other scene if var is set (Combat zone to Lodge Scene)
            
        }

        public void Respawn()
        {
            // Show Player Sprites
            // Reset Player's Health
            // Enable Player Input
        }
    }
}

