using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using Player.Input;
using TMPro;
using UnityEngine;
using UnityEngine.SearchService;
using UnityEngine.UI;


namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        //Choices
        [SerializeField] private VerticalLayoutGroup _choiceButtonContainer;

        [SerializeField] private Button _choiceButtonPrefab;
        //Choices
        //Tags
        private const string SPEAKER_TAG ="speaker";
        private const string PORTRAIT_TAG = "portrait";
        private const string LAYOUT_TAG = "layout";
        //Tags
        [Header("Dialogue UI")]

        [SerializeField] private GameObject dialoguePanel;

        [SerializeField] private TextMeshProUGUI dialogueText;
    
        [SerializeField] private PlayerInput playerInput;

        private Story _currentStory;

        private bool _dialogueIsPlaying;

        private static DialogueManager _instance;
    
        public bool interactPressed => _interactPressed;
        private bool _interactPressed;

        public bool _canInteract = true;

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
           // checks if choices are already being displaye
            if (_choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0) return;
            for (int i = 0; i < _currentStory.currentChoices.Count; i++) // iterates through all choices
            {
                var choice = _currentStory.currentChoices[i];
                var button = CreateChoiceButton(choice.text); // creates a choice button

                button.onClick.AddListener(() => OnClickChoiceButton(choice));
            }
        }
        
        Button CreateChoiceButton(string text)
{
  // creates the button from a prefab
  var choiceButton = Instantiate(_choiceButtonPrefab);
  choiceButton.transform.SetParent(_choiceButtonContainer.transform, false);
  
  // sets text on the button
  var buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
  buttonText.text = text;

  return choiceButton;
}

    void OnClickChoiceButton(Choice choice)
{
  _currentStory.ChooseChoiceIndex(choice.index); // tells ink which choice was selected
  RefreshChoiceView(); // removes choices from the screen
  DisplayNextLine();

}

void RefreshChoiceView()
{
  if (_choiceButtonContainer != null)
  {
    foreach (var button in _choiceButtonContainer.GetComponentsInChildren<Button>())
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
     HandleTags(_currentStory.currentTags);//Tags
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
            Debug.LogError("Tag could not be appropriately parsed: " +tag);
        }
        string tagKey = splitTag[0].Trim();
        string tagValue = splitTag[1].Trim();
        //handle the tag
        switch (tagKey)
        {
            case SPEAKER_TAG:
                Debug.Log("speaker=" + tagValue);
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
        

        IEnumerator CanInteract()
        {
            _canInteract = false;
            yield return new WaitForSeconds(1f);
            _canInteract = true;
        } 

        public void EnterDialogueMode(TextAsset inkJson)
        {
            _currentStory = new Story(inkJson.text);
            _dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);
            playerInput.enabled=false;
            DisplayNextLine();
        }

        private void ExitDialogueMode()
        {
            _dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
            playerInput.enabled=true;
        }

        

  
    }
}