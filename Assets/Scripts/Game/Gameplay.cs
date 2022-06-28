using System;
using System.Collections;
using Game.Garbage;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Gameplay
    {
        public const int MistakeCount = 3;
        public event EventHandler GameStarted;
        public event EventHandler GameOver;
        public event EventHandler<int> ItemCaught; 

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
            
            OnGameStarted();

            return GarbageGenerator.GeneratorTask();
        }

        public void HandleCorrect(int type)
        {
            Debug.Log("Correct");
            GameState.IncrementCorrect();
            OnItemCaught(type);
        }

        public void HandleIncorrect(int type)
        {
            Debug.Log("Incorrect");
            GameState.IncrementIncorrect();
            OnItemCaught(type);
            CheckGameOver();
        }

        public void HandleMissed()
        {
            Debug.Log("Missed");
            GameState.IncrementMissed();
            CheckGameOver();
        }

        private void OnGameStarted()
        {
            GameStarted?.Invoke(this, EventArgs.Empty);
        }
        
        private void OnGameOver()
        {
            GameOver?.Invoke(this, EventArgs.Empty);
        }

        private void OnItemCaught(int type)
        {
            ItemCaught?.Invoke(this, type);
        }

        private void CheckGameOver()
        {
            if (GameState.Incorrect + GameState.Missed == GameMode.MistakeCount)
            {
                GameState.Stop();
                GarbageGenerator.Stop();
                OnGameOver();
            }
        }
    }
}