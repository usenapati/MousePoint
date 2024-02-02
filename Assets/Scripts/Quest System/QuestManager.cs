using System;
using System.Collections.Generic;
using Core.Managers;
using UnityEngine;

namespace Quest_System
{
    public class QuestManager : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private bool loadQuestState = true;
        
        private Dictionary<string, Quest> _questMap;
        
        public static QuestManager instance { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            if (instance != null)
            {
                Debug.LogError("Found more than one Quest Manager in the scene.");
                Destroy(gameObject);
            }

            instance = this;
            _questMap = CreateQuestMap();
            
        }
        

        private void OnEnable()
        {
            GameEventsManager.instance.questEvents.OnStartQuest += StartQuest;
            GameEventsManager.instance.questEvents.OnAdvanceQuest += AdvanceQuest;
            GameEventsManager.instance.questEvents.OnFinishQuest += FinishQuest;
            
            GameEventsManager.instance.questEvents.OnQuestStepStateChange += QuestStepStateChange;
        }

        private void OnDisable()
        {
            GameEventsManager.instance.questEvents.OnStartQuest -= StartQuest;
            GameEventsManager.instance.questEvents.OnAdvanceQuest -= AdvanceQuest;
            GameEventsManager.instance.questEvents.OnFinishQuest -= FinishQuest;
            
            GameEventsManager.instance.questEvents.OnQuestStepStateChange -= QuestStepStateChange;
        }

        private void Start()
        {
            
            foreach (Quest quest in _questMap.Values)
            {
                // initialize any loaded quest steps
                if (quest.state == QuestState.InProgress)
                {
                    quest.InstantiateCurrentQuestStep(this.transform);
                }
                // broadcast the initial state of all quests on startup
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
                if (GetQuestById(prerequisiteQuestInfo.id).state != QuestState.Finished)
                {
                    meetsRequirements = false;
                    break;
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
                if (quest.state == QuestState.RequirementsNotMet && CheckRequirementsMet(quest))
                {
                    ChangeQuestState(quest.info.id, QuestState.CanStart);
                }
            }
        }

        private void StartQuest(string id) 
        {
            Quest quest = GetQuestById(id);
            quest.InstantiateCurrentQuestStep(this.transform);
            ChangeQuestState(quest.info.id, QuestState.InProgress);
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
                ChangeQuestState(quest.info.id, QuestState.CanFinish);
            }
        }

        private void FinishQuest(string id)
        {
            Quest quest = GetQuestById(id);
            ChangeQuestState(quest.info.id, QuestState.Finished);
        }
        
        private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
        {
            Quest quest = GetQuestById(id);
            quest.StoreQuestStepState(questStepState, stepIndex);
            ChangeQuestState(id, quest.state);
        }

        private Dictionary<string, Quest> CreateQuestMap()
        {
            // loads all QuestInfoSO Scriptable Objects under the Assets/Resources/Quests folder
            QuestInfoSO[] allQuests = UnityEngine.Resources.LoadAll<QuestInfoSO>("Quests");
            // Create the quest map
            Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
            foreach (QuestInfoSO questInfo in allQuests)
            {
                if (idToQuestMap.ContainsKey(questInfo.id))
                {
                    Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
                }
                idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
            }
            return idToQuestMap;
        }

        public Quest GetQuestById(string id)
        {
            Quest quest = _questMap[id];
            if (quest == null)
            {
                Debug.LogError("ID not found in the Quest Map: " + id);
            }

            return quest;
        }
        
        private void OnApplicationQuit()
        {
            foreach (Quest quest in _questMap.Values)
            {
                SaveQuest(quest);
            }
        }

        private void SaveQuest(Quest quest)
        {
            try 
            {
                QuestData questData = quest.GetQuestData();
                // serialize using JsonUtility, but use whatever you want here (like JSON.NET)
                string serializedData = JsonUtility.ToJson(questData);
                // saving to PlayerPrefs is just a quick example for this tutorial video,
                // you probably don't want to save this info there long-term.
                // instead, use an actual Save & Load system and write to a file, the cloud, etc..
                PlayerPrefs.SetString(quest.info.id, serializedData);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to save quest with id " + quest.info.id + ": " + e);
            }
        }

        private Quest LoadQuest(QuestInfoSO questInfo)
        {
            Quest quest = null;
            try 
            {
                // load quest from saved data
                if (PlayerPrefs.HasKey(questInfo.id) && loadQuestState)
                {
                    string serializedData = PlayerPrefs.GetString(questInfo.id);
                    QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                    quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
                }
                // otherwise, initialize a new quest
                else 
                {
                    quest = new Quest(questInfo);
                }
            }
            catch (Exception e)
            {
                if (quest != null) Debug.LogError("Failed to load quest with id " + quest.info.id + ": " + e);
            }
            return quest;
        }
    }
}
