using Dialogue;
using Events;
using UnityEngine;

namespace Core.Managers
{
    public class GameEventsManager : MonoBehaviour
    {
        public static GameEventsManager instance { get; private set; }
        
        public DialogueEvents dialogueEvents;
        public PlayerEvents playerEvents;
        public InputEvents inputEvents;
        public EnemyEvents enemyEvents;
        public EnvironmentEvents environmentEvents;
        public QuestEvents questEvents;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one Game Events Manager in the scene.");
            }

            instance = this;

            playerEvents = new PlayerEvents();
            inputEvents = new InputEvents();
            enemyEvents = new EnemyEvents();
            environmentEvents = new EnvironmentEvents();
            questEvents = new QuestEvents();
            dialogueEvents = new DialogueEvents();
        }
    }
}
