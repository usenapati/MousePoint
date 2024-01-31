using UnityEngine;

namespace Quest_System
{
    public class Quest
    {
        // static info
        public QuestInfoSO info;

        // state info
        public QuestState state;
        private int _currentQuestStepIndex;
        private QuestStepState[] _questStepStates;

        public Quest(QuestInfoSO questInfo)
        {
            info = questInfo;
            state = QuestState.RequirementsNotMet;
            _currentQuestStepIndex = 0;
            _questStepStates = new QuestStepState[info.questStepPrefabs.Length];
            for (int i = 0; i < _questStepStates.Length; i++)
            {
                _questStepStates[i] = new QuestStepState();
            }
        }

        public Quest(QuestInfoSO questInfo, QuestState questState, int currentQuestStepIndex,
            QuestStepState[] questStepStates)
        {
            info = questInfo;
            state = questState;
            this._currentQuestStepIndex = currentQuestStepIndex;
            this._questStepStates = questStepStates;

            // if the quest step states and prefabs are different lengths,
            // something has changed during development and the saved data is out of sync.
            if (this._questStepStates.Length != info.questStepPrefabs.Length)
            {
                Debug.LogWarning("Quest Step Prefabs and Quest Step States are "
                                 + "of different lengths. This indicates something changed "
                                 + "with the QuestInfo and the saved data is now out of sync. "
                                 + "Reset your data - as this might cause issues. QuestId: " + info.id);
            }
        }

        public void MoveToNextStep()
        {
            _currentQuestStepIndex++;
        }

        public bool CurrentStepExists()
        {
            return (_currentQuestStepIndex < info.questStepPrefabs.Length);
        }

        public void InstantiateCurrentQuestStep(Transform parentTransform)
        {
            GameObject questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null)
            {
                QuestStep questStep = Object.Instantiate(questStepPrefab, parentTransform)
                    .GetComponent<QuestStep>();
                questStep.InitializeQuestStep(info.id, _currentQuestStepIndex,
                    _questStepStates[_currentQuestStepIndex].state);
            }
        }

        private GameObject GetCurrentQuestStepPrefab()
        {
            GameObject questStepPrefab = null;
            if (CurrentStepExists())
            {
                questStepPrefab = info.questStepPrefabs[_currentQuestStepIndex];
            }
            else
            {
                Debug.LogWarning("Tried to get quest step prefab, but stepIndex was out of range indicating that "
                                 + "there's no current step: QuestId=" + info.id + ", stepIndex=" +
                                 _currentQuestStepIndex);
            }

            return questStepPrefab;
        }

        public void StoreQuestStepState(QuestStepState questStepState, int stepIndex)
        {
            if (stepIndex < _questStepStates.Length)
            {
                _questStepStates[stepIndex].state = questStepState.state;
            }
            else
            {
                Debug.LogWarning("Tried to access quest step data, but stepIndex was out of range: "
                                 + "Quest Id = " + info.id + ", Step Index = " + stepIndex);
            }
        }

        public QuestData GetQuestData()
        {
            return new QuestData(state, _currentQuestStepIndex, _questStepStates);
        }
    }
}