using System;
using Quest_System;
using UnityEngine;

namespace Resources.Quests.CollectItemQuest
{
    public class CollectItemsQuestStep : QuestStep
    {
        private int itemsCollected = 0;

        private int itemsToComplete = 5;

        private void OnEnable()
        {
            // Subscribe to item pick up event
        }

        private void OnDisable()
        {
            // Unsubscribe
        }

        private void ItemCollected()
        {
            if (itemsCollected < itemsToComplete)
            {
                itemsCollected++;
            }
            else
            {
                FinishQuestStep();
            }
        }
    }
}
