using Core.Managers;
using UnityEngine;

namespace Quest_System
{
    [RequireComponent(typeof(SphereCollider))]
    public class QuestPoint : MonoBehaviour
    {
        [Header("Quest")]
        [SerializeField] private QuestInfoSO questInfoForPoint;

        [Header("Config")]
        [SerializeField] private bool startPoint = true;
        [SerializeField] private bool finishPoint = true;

        private bool _playerIsNear;
        private string _questId;
        private QuestState _currentQuestState;

        private QuestIcon _questIcon;

        private void Awake() 
        {
            _questId = questInfoForPoint.id;
            _questIcon = GetComponentInChildren<QuestIcon>();
        }

        private void OnEnable()
        {
            GameEventsManager.instance.questEvents.OnQuestStateChange += QuestStateChange;
            GameEventsManager.instance.inputEvents.OnInteractPressed += InteractPressed;
        }

        private void OnDisable()
        {
            GameEventsManager.instance.questEvents.OnQuestStateChange -= QuestStateChange;
            GameEventsManager.instance.inputEvents.OnInteractPressed -= InteractPressed;
        }

        private void InteractPressed()
        {
            if (!_playerIsNear)
            {
                return;
            }

            // start or finish a quest
            if (_currentQuestState.Equals(QuestState.CanStart) && startPoint)
            {
                GameEventsManager.instance.questEvents.StartQuest(_questId);
            }
            else if (_currentQuestState.Equals(QuestState.CanFinish) && finishPoint)
            {
                GameEventsManager.instance.questEvents.FinishQuest(_questId);
            }
        }

        private void QuestStateChange(Quest quest)
        {
            // only update the quest state if this point has the corresponding quest
            if (quest.info.id.Equals(_questId))
            {
                _currentQuestState = quest.state;
                _questIcon.SetState(_currentQuestState, startPoint, finishPoint);
            }
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
            {
                _playerIsNear = true;
            }
        }

        private void OnTriggerExit(Collider otherCollider)
        {
            if (otherCollider.CompareTag("Player"))
            {
                _playerIsNear = false;
            }
        }

        public void UpdateQuest(string id)
        {
            var newQuest = QuestManager.instance.GetQuestById(id);
            if (newQuest != null)
            {
                questInfoForPoint = newQuest.info;
                _questId = newQuest.info.id;
                _currentQuestState = newQuest.state;
            }
        }
    }
}
