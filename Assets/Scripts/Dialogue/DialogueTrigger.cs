using System;
using UnityEngine;

namespace Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("VisualCue")]
        [SerializeField] private GameObject visualCue;

        [Header("Ink JSON")]
        [SerializeField] private TextAsset inkJSON;

        private bool hasStartedDialogue = false;

        private void Start()
        {
            visualCue.SetActive(false);
        }
        
        
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) // Corrected line
            {
                if (!hasStartedDialogue && DialogueManager.GetInstance().interactPressed)
                {
                    DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                    hasStartedDialogue = true;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (!hasStartedDialogue && other.gameObject.CompareTag("Player")) // Corrected line
            {
                visualCue.SetActive(true);
                
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) // Corrected line
            {
                visualCue.SetActive(false);
                hasStartedDialogue = false;
            }
        }
    }
}