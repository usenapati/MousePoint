using System;
using Dialogue;
using Quest_System;
using UnityEngine;

namespace NPC
{
    public class NPCManager : MonoBehaviour
    {
        [Header("Dialogues")] 
        [SerializeField] private TextAsset[] dialogues;

        [Header("Quests")] 
        [SerializeField] private QuestDialogueInfoSO[] quests;

        private DialogueTrigger _dialogueTrigger;
        private QuestPoint _questPoint;

        private int _currentDialogueIndex;

        private void Start()
        {
            // Get Dialogue Trigger and Quest Point Components (Need to be in same game object)
        }

        private void OnEnable()
        {
            // Listen to quest finish event
            // Listen to dialogue finish event
            
        }

        private void OnDisable()
        {
            
            
        }

        public void ProgressDialogue()
        {
            // Increment dialogueIndex once event has happened (Finished Quest or other Event is called)
        }

        public void AddQuest()
        {
            // Check currentDialogueIndex and add quest based on index
        }

        public void UpdateDialogueTrigger()
        {
            // Change Dialogue Trigger's TextAsset based on current dialogue
        }

        public void UpdateQuestPoint()
        {
            // Change Quest Point's quest based on current dialogue index and related quest
        }
    }
}
