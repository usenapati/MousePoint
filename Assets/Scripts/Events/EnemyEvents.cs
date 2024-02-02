using System;
using UnityEngine;

namespace Events
{
    public class EnemyEvents
    {
        public event Action OnEnemyDefeated;

        public void EnemyDefeated()
        {
            
            OnEnemyDefeated?.Invoke();
        }
    }
}
