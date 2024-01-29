using System;
using System.Collections.Generic;
using Core.Managers;
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

        private void OnEnable()
        {
            GameEventsManager.instance.questEvents.OnStartQuest += StartQuest;
            GameEventsManager.instance.questEvents.OnAdvanceQuest += AdvanceQuest;
            GameEventsManager.instance.questEvents.OnFinishQuest += FinishQuest;
        }

        private void OnDisable()
        {
            GameEventsManager.instance.questEvents.OnStartQuest -= StartQuest;
            GameEventsManager.instance.questEvents.OnAdvanceQuest -= AdvanceQuest;
            GameEventsManager.instance.questEvents.OnFinishQuest -= FinishQuest;
        }

        private void Start()
        {
            foreach (Quest quest in _questMap.Values)
            {
                GameEventsManager.instance.questEvents.QuestStateChange(quest);
            }
        }

        private void ChangeQuestState(string id, QuestState state)
        {
            Quest quest = GetQuestById(id);
            quest.state = state;
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }

        private bool CheckRequirementsMet(Quest quest)
        {
            bool meetsRequirements = true;
            
            
            // Potential Player Level Check

            foreach (var prerequisiteQuestInfo in quest.info.questPrerequisites)
            {
                if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.FINISHED)
                {
                    return false;
                }
            }

            return meetsRequirements;
        }

        private void Update()
        {
            // loop through ALL quests
            foreach (Quest quest in _questMap.Values)
            {
                // if we're now meeting the requirements, switch over to the CAN_START state
                if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
                {
                    ChangeQuestState(quest.info.id, QuestState.CAN_START);
                }
            }
        }

        private void StartQuest(string id) 
        {
            Quest quest = GetQuestById(id);
            quest.InstantiateCurrentQuestStep(this.transform);
            ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
        }

        private void AdvanceQuest(string id)
        {
            Quest quest = GetQuestById(id);

            // move on to the next step
            quest.MoveToNextStep();

            // if there are more steps, instantiate the next one
            if (quest.CurrentStepExists())
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }
            // if there are no more steps, then we've finished all of them for this quest
            else
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
            }
        }

        private void FinishQuest(string id)
        {
            Quest quest = GetQuestById(id);
            ChangeQuestState(quest.info.id, QuestState.FINISHED);
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
