using Core.Managers;
using UnityEngine;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("VisualCue")]
        [SerializeField] private GameObject visualCue;

        [Header("Ink JSON")]
        [SerializeField] private TextAsset inkJSON;

        private bool _hasStartedDialogue;

        private void Start()
        {
            visualCue.SetActive(false);
        }
        
        
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) // Corrected line
            {
                if (!_hasStartedDialogue && DialogueManager.GetInstance().interactPressed)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                    GameEventsManager.instance.dialogueEvents.DialogueStarted(inkJSON.name);
                    _hasStartedDialogue = true;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (!_hasStartedDialogue && other.gameObject.CompareTag("Player")) // Corrected line
            {
                visualCue.SetActive(true);
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) // Corrected line
            {
                visualCue.SetActive(false);
            }
        }

        public void SetDialogueText(TextAsset dialogueText)
        {
            if (dialogueText != null)
            {
                inkJSON = dialogueText;
            }
        }
    }
}