using Core.Managers;

namespace Health
{
    public class EnemyHealth : Health
    {
        protected override void Die()
        {
            GameEventsManager.instance.enemyEvents.EnemyDefeated();
            base.Die();
            Destroy(transform.root.gameObject);
        }
    }
}

