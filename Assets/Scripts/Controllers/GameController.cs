using System;
using Animation;
using Game;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameplayAnimation gameplayAnimation;
        [SerializeField] private GameplayUIController gameplayUIController;

        private Gameplay _gameplay;
        private GameMode _gameMode;

        [Inject]
        public void Construct(
            CrossSceneContainer crossSceneContainer,
            Gameplay gameplay
        )
        {
            _gameplay = gameplay;
            _gameMode = crossSceneContainer.Pop<GameMode>();
        }

        private void Awake()
        {
            gameplayAnimation.AnimationFinished += OnAnimationFinished;
            _gameMode.TankEnableChanged += TankEnableChanged;
            _gameplay.GameStarted += GameplayOnGameStarted;
            _gameplay.GameOver += GameplayOnGameOver;
        }

        private void OnDestroy()
        {
            gameplayAnimation.AnimationFinished -= OnAnimationFinished;
            _gameMode.TankEnableChanged -= TankEnableChanged;
            _gameplay.GameStarted -= GameplayOnGameStarted;
            _gameplay.GameOver -= GameplayOnGameOver;
        }

        void Start()
        {
            gameplayAnimation.ShowGameplay(_gameMode.GetTankTypes());
            gameplayUIController.Reset();
        }

        private void OnAnimationFinished(object sender, EventArgs e)
        {
            StartCoroutine(_gameplay.GameplayTask(_gameMode));
        }

        private void TankEnableChanged(object sender, TankEnableEventArgs e)
        {
            if (e.NewValue)
            {
                gameplayAnimation.ShowTank(e.Type);
            }
        }

        private void GameplayOnGameStarted(object sender, EventArgs e)
        {
            _gameplay.GameState.CorrectChanged += OnGameStateCorrectChanged;
            _gameplay.GameState.IncorrectChanged += OnGameStateIncorrectChanged;
            _gameplay.GameState.MissedChanged += OnGameStateMissedChanged;

            gameplayUIController.StartTimer(_gameplay.GameState.StartTime);
        }

        private void GameplayOnGameOver(object sender, EventArgs e)
        {
            Time.timeScale = 0f;
            gameplayUIController.ShowGameOver();
            gameplayUIController.StopTimer();
            
            _gameplay.GameState.CorrectChanged -= OnGameStateCorrectChanged;
            _gameplay.GameState.IncorrectChanged -= OnGameStateIncorrectChanged;
            _gameplay.GameState.MissedChanged -= OnGameStateMissedChanged;
        }

        private void OnGameStateCorrectChanged(object sender, int value)
        {
            gameplayUIController.SetCorrectCount(value);
            gameplayUIController.ShowCorrect();
        }

        private void OnGameStateIncorrectChanged(object sender, int value)
        {
            gameplayUIController.SetIncorrectCount(value);
            gameplayUIController.ShowIncorrect();
        }

        private void OnGameStateMissedChanged(object sender, int value)
        {
            gameplayUIController.SetMissedCount(value);
            gameplayUIController.ShowMissed();
        }
    }
}