using System.Collections;
using Game.Garbage;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Gameplay
    {
        public GameMode GameMode { get; private set; }
        public GameState GameState { get; private set; }
        public GarbageGenerator GarbageGenerator { get; private set; }

        [Inject]
        private void Construct(GarbageGenerator garbageGenerator)
        {
            GarbageGenerator = garbageGenerator;
        }

        public IEnumerator GameplayTask(GameMode gameMode)
        {
            Debug.Log("Start gameplay");
            GameMode = gameMode;
            GameState = new GameState();
            GarbageGenerator.SetGameMode(gameMode);
            
            // TODO Start timer, etc.
            
            return GarbageGenerator.GeneratorTask();
        }

        public void HandleCorrect()
        {
            Debug.Log("Correct");
            GameState.IncrementCorrect();
        }

        public void HandleIncorrect()
        {
            Debug.Log("Incorrect");
            GameState.IncrementIncorrect();
        }

        public void HandleMissed()
        {
            Debug.Log("Missed");
            GameState.IncrementMissed();
        }
    }
}