using UnityEngine;

namespace Quest_System
{
    public class QuestIcon : MonoBehaviour
    {
        [Header("Icons")]
        [SerializeField] private GameObject requirementsNotMetToStartIcon;
        [SerializeField] private GameObject canStartIcon;
        [SerializeField] private GameObject requirementsNotMetToFinishIcon;
        [SerializeField] private GameObject canFinishIcon;

        public void SetState(QuestState newState, bool startPoint, bool finishPoint)
        {
            // set all to inactive
            requirementsNotMetToStartIcon.SetActive(false);
            canStartIcon.SetActive(false);
            requirementsNotMetToFinishIcon.SetActive(false);
            canFinishIcon.SetActive(false);

            // set the appropriate one to active based on the new state
            switch (newState)
            {
                case QuestState.RequirementsNotMet:
                    if (startPoint) { requirementsNotMetToStartIcon.SetActive(true); }
                    break;
                case QuestState.CanStart:
                    if (startPoint) { canStartIcon.SetActive(true); }
                    break;
                case QuestState.InProgress:
                    if (finishPoint) { requirementsNotMetToFinishIcon.SetActive(true); }
                    break;
                case QuestState.CanFinish:
                    if (finishPoint) { canFinishIcon.SetActive(true); }
                    break;
                case QuestState.Finished:
                    break;
                default:
                    Debug.LogWarning("Quest State not recognized by switch statement for quest icon: " + newState);
                    break;
            }
        }
    }
}
