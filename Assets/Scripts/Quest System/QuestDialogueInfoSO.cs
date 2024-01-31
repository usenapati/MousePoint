using UnityEngine;
using UnityEngine.Serialization;

namespace Quest_System
{
    [CreateAssetMenu(fileName = "QuestDialogueInfoSO", menuName = "Quests/QuestDialogueInfoSO")]
    public class QuestDialogueInfoSO : ScriptableObject
    {
        public TextAsset dialogue;
        public QuestInfoSO questInfoSo;
    }
}
