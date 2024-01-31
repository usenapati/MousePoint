using System;

namespace Events
{
    public class DialogueEvents
    {
        public event Action<string> OnDialogueStarted;
        public event Action<string> OnDialogueCompleted;
        // Dialogue should have an id (global) and id (quest)
        // Started
        public void DialogueStarted(string name)
        {
            OnDialogueStarted?.Invoke(name);
        }
        // Completed
        public void DialogueCompleted(string name)
        {
            OnDialogueCompleted?.Invoke(name);
        }
    }
}
