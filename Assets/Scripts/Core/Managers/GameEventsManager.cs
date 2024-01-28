using System;
using Dialogue;
using Player.Input;
using UnityEngine;

namespace Core.Managers
{
    public class GameEventsManager : MonoBehaviour
    {
        public static GameEventsManager instance { get; private set; }

        public InputManager inputManager;
        public DialogueManager dialogueManager;
        public QuestEvents questEvents;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Found more than one Game Events Manager in the scene.");
            }

            instance = this;
            
            // Initialize Events
        }
    }
}
