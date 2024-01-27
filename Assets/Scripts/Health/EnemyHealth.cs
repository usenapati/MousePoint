namespace Health
{
    public class EnemyHealth : Health
    {
        protected override void Die()
        {
            base.Die();
            Destroy(transform.root.gameObject);
        }
    }
}

