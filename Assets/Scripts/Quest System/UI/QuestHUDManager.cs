using System;
using UnityEngine;

namespace Quest_System.UI
{
    public class QuestHUDManager : MonoBehaviour
    {
        // List of current quests
        // Quest HUD Item Prefab
        // - Name
        // - Progress/Steps
        // - Quest Status
        // Listen to quest updates

        private void OnEnable()
        {
            throw new NotImplementedException();
        }

        private void OnDisable()
        {
            throw new NotImplementedException();
        }

        // Called whenever a quest is started or completed
        public void UpdateQuestList()
        {
            // Check quests that child under the manager
            // Add them to list
            // Account for dupes
            // Create Quest HUD Item based off of list
        }
        
    }
}
