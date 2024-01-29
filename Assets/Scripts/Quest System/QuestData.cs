namespace Quest_System
{
    [System.Serializable]
    public class QuestData
    {
        public QuestState state;
        public int questStepIndex;
        public QuestStepState[] questStepStates;

        public QuestData(QuestState state, int questStepIndex, QuestStepState[] questStepStates)
        {
            this.state = state;
            this.questStepIndex = questStepIndex;
            this.questStepStates = questStepStates;
        }
    }
}