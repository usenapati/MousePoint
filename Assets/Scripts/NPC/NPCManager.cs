using System.Linq;
using Core.Managers;
using Dialogue;
using Quest_System;
using UnityEngine;

namespace NPC
{
    public class NPCManager : MonoBehaviour
    {
        [Header("Dialogues")] [SerializeField] private TextAsset[] dialogues;

        [Header("Quests")] [SerializeField] private QuestDialogueInfoSO[] quests;

        private DialogueTrigger _dialogueTrigger;
        private QuestPoint _questPoint;

        private int _currentDialogueIndex;
        private string _currentQuestID;

        private void Start()
        {
            // Get Dialogue Trigger and Quest Point Components (Need to be in same game object)
            _dialogueTrigger = GetComponent<DialogueTrigger>();
            _questPoint = GetComponent<QuestPoint>();
        }

        private void OnEnable()
        {
            // Listen to quest finish event
            GameEventsManager.instance.questEvents.OnFinishQuest += ProgressDialogueAfterQuest;
            // Listen to dialogue finish event
            GameEventsManager.instance.dialogueEvents.OnDialogueCompleted += AddQuest;
        }

        private void OnDisable()
        {
            // Listen to quest finish event
            GameEventsManager.instance.questEvents.OnFinishQuest -= ProgressDialogueAfterQuest;
            // Listen to dialogue finish event
            GameEventsManager.instance.dialogueEvents.OnDialogueCompleted -= AddQuest;
        }

        private void ProgressDialogueAfterQuest(string id)
        {
            // Increment dialogueIndex once event has happened (Finished Quest or other Event is called)
            if (QuestManager.instance.GetQuestById(id).state == QuestState.Finished)
            {
                _currentDialogueIndex++;
                UpdateDialogueTrigger();
            }
        }

        private void AddQuest(string dialogueName)
        {
            // Check currentDialogueIndex and add quest based on index
            var completedDialogue = quests.First(i => i.dialogue.name == dialogueName);
            if (completedDialogue != null)
            {
                _currentQuestID = completedDialogue.questInfoSo.id;
                if (QuestManager.instance.GetQuestById(_currentQuestID).state == QuestState.CanStart)
                {
                    UpdateQuestPoint();
                }
            }
        }

        private void UpdateDialogueTrigger()
        {
            // Change Dialogue Trigger's TextAsset based on current dialogue
            if (dialogues.Length > _currentDialogueIndex && _currentDialogueIndex >= 0)
            {
                _dialogueTrigger.SetDialogueText(dialogues[_currentDialogueIndex]);
            }
        }

        private void UpdateQuestPoint()
        {
            // Change Quest Point's quest based on current dialogue index and related quest
            _questPoint.UpdateQuest(_currentQuestID);
        }
    }
}