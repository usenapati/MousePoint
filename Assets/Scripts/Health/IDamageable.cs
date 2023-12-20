namespace Health
{
    public interface IDamageable
    {
        /// <summary>
        /// Damages something. Returns true if successful, false otherwise
        /// </summary>
        bool Damage(float amount);
    }
}
