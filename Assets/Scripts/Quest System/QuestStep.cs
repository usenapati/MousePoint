using UnityEngine;

namespace Quest_System
{
    public abstract class QuestStep : MonoBehaviour
    {
        private bool _isFinished = false;

        protected void FinishQuestStep()
        {
            if (!_isFinished)
            {
                _isFinished = true;
                
                // TODO - Advance quest forward
                
                Destroy(gameObject);
            }
        }
    }
}
