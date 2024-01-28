using UnityEngine;

namespace Quest_System
{
    public class Quest
    {
        public QuestInfoSO info;
        
        // state info
        public QuestState state;
        private int currentQuestStepIndex;

        public Quest(QuestInfoSO questInfo)
        {
            info = questInfo;
            state = QuestState.REQUIREMENTS_NOT_MET;
            this.currentQuestStepIndex = 0;
        }

        public void MoveToNextStep()
        {
            currentQuestStepIndex++;
        }

        public bool CurrentStepExists()
        {
            return (currentQuestStepIndex < info.questStepPrefabs.Length);
        }

        public void InstantiateCurrentQuestStep(Transform parentTransform)
        {
            GameObject questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null)
            {
                Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
            }
        }

        private GameObject GetCurrentQuestStepPrefab()
        {
            GameObject questStepPrefab = null;
            if (CurrentStepExists())
            {
                questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
            }
            else
            {
                Debug.LogWarning("Tried to get quest step prefab, stepIndex is out of range. " +
                                 "QuestId= " + info.id + ", stepIndex= " + currentQuestStepIndex);
            }

            return questStepPrefab;
        }
    }
}
