namespace Quest_System
{
    [System.Serializable]
    public class QuestStepState
    {
        public string state;

        public QuestStepState(string state)
        {
            this.state = state;
        }

        public QuestStepState()
        {
            this.state = "";
        }
    }
}