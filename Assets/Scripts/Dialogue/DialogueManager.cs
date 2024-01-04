using Ink.Runtime;
using Player.Input;
using TMPro;
using UnityEngine;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Dialogue UI")]

        [SerializeField] private GameObject dialoguePanel;

        [SerializeField] private TextMeshProUGUI dialogueText;
    
        [SerializeField] private PlayerInput playerInput;

        private Story _currentStory;

        private bool _dialogueIsPlaying;

        private static DialogueManager _instance;
    
        public bool interactPressed => _interactPressed;
        private bool _interactPressed;

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning("Found more than one dialogue manager in the scene");
            }

            _instance = this;
        }
        public static DialogueManager GetInstance()
        {
            return _instance;
        }

        private void OnEnable()
        {
            playerInput.InteractEvent += HandleInteract;
        }

        private void OnDisable()
        {
            playerInput.InteractEvent -= HandleInteract;
        }
    
        private void HandleInteract(bool isPressed)
        {
            _interactPressed = isPressed;
        }

        private void Start()
        {
            _dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
        }

        private void Update()
        {
            //return right away if dialogue isn't playing
            if (!_dialogueIsPlaying)
            {
                return;
            }

            // handle continuing to the next line in the dialogue when submit is pressed
            if (interactPressed)
            {
                ContinueStory();
            }
        }



        public void EnterDialogueMode(TextAsset inkJson)
        {
            _currentStory = new Story(inkJson.text);
            _dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);

            ContinueStory();
        }

        private void ExitDialogueMode()
        {
            _dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
        }

        private void ContinueStory()
        {
            if (_currentStory.canContinue)
            {
                dialogueText.text = _currentStory.Continue();
            }
            else
            {
                ExitDialogueMode();
            }

        }

  
    }
}