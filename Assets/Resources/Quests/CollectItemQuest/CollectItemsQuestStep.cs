using System;
using Core.Managers;
using Quest_System;
using UnityEngine;

namespace Resources.Quests.CollectItemQuest
{
    public class CollectItemsQuestStep : QuestStep
    {
        private string _itemName = "Berries";
        
        private int _itemsCollected = 0;

        private int _itemsToComplete = 5;

        private void OnEnable()
        {
            GameEventsManager.instance.environmentEvents.OnItemGained += ItemCollected;
        }

        private void OnDisable()
        {
            GameEventsManager.instance.environmentEvents.OnItemGained -= ItemCollected;
        }

        private void ItemCollected(string item)
        {
            // Need a list of required items - Ex: Berries
            if (_itemsCollected < _itemsToComplete && item == _itemName)
            {
                _itemsCollected++;
            }
            else
            {
                FinishQuestStep();
            }
        }

        private void UpdateState()
        {
            string state = _itemsCollected.ToString();
            ChangeState(state);
        }

        protected override void SetQuestStepState(string state)
        {
            _itemsCollected = Int32.Parse(state);
            UpdateState();
        }
    }
}
