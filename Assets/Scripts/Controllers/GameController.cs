using System;
using Animation;
using Game;
using Services;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameplayAnimation gameplayAnimation;
        [SerializeField] private GameplayUIController gameplayUIController;

        private Gameplay _gameplay;
        private GameMode _gameMode;
        private AudioService _audioService;
        private RecordsService _recordsService;
        private SceneSwitcher _sceneSwitcher;

        [Inject]
        public void Construct(
            CrossSceneContainer crossSceneContainer,
            Gameplay gameplay,
            AudioService audioService,
            RecordsService recordsService,
            SceneSwitcher sceneSwitcher
        )
        {
            _gameplay = gameplay;
            _gameMode = crossSceneContainer.Pop<GameMode>();
            _audioService = audioService;
            _recordsService = recordsService;
            _sceneSwitcher = sceneSwitcher;
        }

        private void Awake()
        {
            gameplayAnimation.AnimationFinished += OnAnimationFinished;
            _gameMode.TankEnableChanged += TankEnableChanged;
            _gameplay.GameStarted += GameplayOnGameStarted;
            _gameplay.GameOver += GameplayOnGameOver;
            _gameplay.ItemCaught += GameplayOnItemCaught;
        }

        private void GameplayOnItemCaught(object sender, int e)
        {
            _audioService.PlaySound(GetSoundIndex(e));
        }

        private int GetSoundIndex(int type)
        {
            switch (type)
            {
                case 0:
                    return Random.Range(5, 7);
                case 1:
                    return 1;
                case 2:
                    return 7;
                case 3:
                    return 4;
                case 4:
                    return 0;
                case 5:
                    return Random.Range(2, 4);
                default:
                    return 3;
            }
        }

        private void OnDestroy()
        {
            gameplayAnimation.AnimationFinished -= OnAnimationFinished;
            _gameMode.TankEnableChanged -= TankEnableChanged;
            _gameplay.GameStarted -= GameplayOnGameStarted;
            _gameplay.GameOver -= GameplayOnGameOver;
            _gameplay.ItemCaught -= GameplayOnItemCaught;
        }

        void Start()
        {
            gameplayAnimation.ShowGameplay(_gameMode.GetTankTypes());
            gameplayUIController.Reset();
        }

        public void OnGameOverOkClick()
        {
            _sceneSwitcher.SwitchScene("Game Select Scene");
            Time.timeScale = 1f;
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

            var state = _gameplay.GameState;
            var record = state.MakeRecord(_gameplay.GameMode.GameType);
            var position = _recordsService.AddRecord(record);
            
            gameplayUIController.ShowGameOver(record, position);
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