using UnityEngine;

namespace Quest_System
{
    [CreateAssetMenu(fileName = "QuestInfoSO", menuName = "Quests/QuestInfoSO")]
    public class QuestInfoSO : ScriptableObject
    {
        [field: SerializeField] public string id { get; private set; }

        [Header("General")]
        public string displayName;

        [Header("Requirements")]
        public QuestInfoSO[] questPrerequisites;

        [Header("Steps")]
        public GameObject[] questStepPrefabs;

        // Rewards

        // ensure the id is always the name of the Scriptable Object asset
        private void OnValidate()
        {
            #if UNITY_EDITOR
                id = this.name;
                UnityEditor.EditorUtility.SetDirty(this);
            #endif
        } 
    }
}
