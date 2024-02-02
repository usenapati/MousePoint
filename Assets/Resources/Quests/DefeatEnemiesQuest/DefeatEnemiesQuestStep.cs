using System;
using Core.Managers;
using Quest_System;
using UnityEngine;

namespace Resources.Quests.DefeatEnemiesQuest
{
    public class DefeatEnemiesQuestStep : QuestStep
    {
        
        private int _enemiesDefeated = 0;

        private int _enemiesToDefeat = 3;

        private void OnEnable()
        {
            GameEventsManager.instance.enemyEvents.OnEnemyDefeated += EnemyDefeated;
        }

        private void OnDisable()
        {
            GameEventsManager.instance.enemyEvents.OnEnemyDefeated -= EnemyDefeated;
        }
        
        private void EnemyDefeated()
        {
            Debug.Log("Enemy Defeated");
            _enemiesDefeated++;
            if (_enemiesDefeated >= _enemiesToDefeat)
            {
                FinishQuestStep();
            }
        }
        
        private void UpdateState()
        {
            string state = _enemiesDefeated.ToString();
            ChangeState(state);
        }

        protected override void SetQuestStepState(string state)
        {
            _enemiesDefeated = Int32.Parse(state);
            UpdateState();
        }
    }
}
