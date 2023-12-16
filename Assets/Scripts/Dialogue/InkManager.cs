using Ink.Runtime;
using TMPro;
using UnityEngine;

namespace Dialogue
{
    public class InkManager : MonoBehaviour
    {
        [SerializeField]
        private TextAsset inkJsonAsset;
        private Story _story;

        [SerializeField]
        private TMP_Text textField;
    
        void Start()
        {
            StartStory();
        }

        private void StartStory()
        {
            _story = new Story(inkJsonAsset.text);
            DisplayNextLine();
        }
  
        public void DisplayNextLine()
        {
            if (!_story.canContinue) return;
    
            string text = _story.Continue(); // gets next line
            text = text?.Trim(); // removes white space from text
            textField.text = text; // displays new text
        }
    }
}
