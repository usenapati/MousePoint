using System.Collections.Generic;
using Core.Managers;
using Ink.Runtime;
using Player.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        //Choices
        [Header("Choices")] [SerializeField] private VerticalLayoutGroup choiceButtonContainer;

        [SerializeField] private Button choiceButtonPrefab;

        //Choices
        //Tags
        private const string SPEAKER_TAG = "speaker";
        private const string PORTRAIT_TAG = "portrait";

        private const string LAYOUT_TAG = "layout";

        //Tags
        [Header("Dialogue UI")] [SerializeField]
        private TextMeshProUGUI displayNameText;

        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private PlayerInput playerInput;

        private string _inkFileName;
        private Story _currentStory;
        private bool _dialogueIsPlaying;
        private static DialogueManager _instance;

        public bool interactPressed => _interactPressed;
        private bool _interactPressed;

        public bool canInteract = true;

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

        //Choices
        private void DisplayChoices()
        {
            // checks if choices are already being displayed
            if (choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0) return;
            foreach (var choice in _currentStory.currentChoices)
            {
                var button = CreateChoiceButton(choice.text); // creates a choice button

                var choice1 = choice;
                button.onClick.AddListener(() => OnClickChoiceButton(choice1));
            }
        }

        Button CreateChoiceButton(string text)
        {
            // creates the button from a prefab
            var choiceButton = Instantiate(choiceButtonPrefab, choiceButtonContainer.transform, false);

            // sets text on the button
            var buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = text;

            return choiceButton;
        }

        private void OnClickChoiceButton(Choice choice)
        {
            _currentStory.ChooseChoiceIndex(choice.index); // tells ink which choice was selected
            RefreshChoiceView(); // removes choices from the screen
            DisplayNextLine();
        }

        private void RefreshChoiceView()
        {
            if (choiceButtonContainer != null)
            {
                foreach (var button in choiceButtonContainer.GetComponentsInChildren<Button>())
                {
                    Destroy(button.gameObject);
                }
            }
        }

        public void DisplayNextLine()
        {
            if (_currentStory.canContinue)
            {
                string text = _currentStory.Continue(); // gets next line

                text = text?.Trim(); // removes white space from text

                dialogueText.text = text; // displays new text
                HandleTags(_currentStory.currentTags); //Tags
            }
            else if (_currentStory.currentChoices.Count > 0)
            {
                DisplayChoices();
            }
            else
            {
                ExitDialogueMode();
            }
        }

//Tags
        private void HandleTags(List<string> currentTags)
        {
            foreach (string tag in currentTags)
            {
                string[] splitTag = tag.Split(':');
                if (splitTag.Length != 2)
                {
                    Debug.LogError("Tag could not be appropriately parsed: " + tag);
                }

                string tagKey = splitTag[0].Trim();
                string tagValue = splitTag[1].Trim();
                //handle the tag
                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        displayNameText.text = tagValue;
                        //Debug.Log("speaker=" + tagValue);
                        break;
                    case PORTRAIT_TAG:
                        Debug.Log("portrait=" + tagValue);
                        break;
                    case LAYOUT_TAG:
                        Debug.Log("layout=" + tagValue);
                        break;
                    default:
                        Debug.LogWarning("Tag came in but is not currently being handled:" + tag);
                        break;
                }
            }
        }
//Tags

        //Choices
        

        public void EnterDialogueMode(TextAsset inkJson)
        {
            _inkFileName = inkJson.name;
            _currentStory = new Story(inkJson.text);
            _dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);
            playerInput.enabled = false;
            DisplayNextLine();
        }

        private void ExitDialogueMode()
        {
            _dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
            playerInput.enabled = true;
            GameEventsManager.instance.dialogueEvents.DialogueCompleted(_inkFileName);
        }
    }
}