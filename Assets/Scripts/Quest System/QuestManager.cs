using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quest_System
{
    public class QuestManager : MonoBehaviour
    {
        private Dictionary<string, Quest> _questMap;

        private void Awake()
        {
            _questMap = CreateQuestMap();
        }

        private Dictionary<string, Quest> CreateQuestMap()
        {
            QuestInfoSO[] allQuests = UnityEngine.Resources.LoadAll<QuestInfoSO>("Quests");
            Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
            foreach (var questInfo in allQuests)
            {
                if (idToQuestMap.ContainsKey(questInfo.id))
                {
                    Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
                }
                idToQuestMap.Add(questInfo.id, new Quest(questInfo));
            }

            return idToQuestMap;
        }

        private Quest GetQuestById(string id)
        {
            Quest quest = _questMap[id];
            if (quest == null)
            {
                Debug.LogError("ID not found in the Quest Map: " + id);
            }

            return quest;
        }
    }
}
