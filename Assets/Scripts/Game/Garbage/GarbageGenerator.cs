using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Garbage
{
    public class GarbageGenerator
    {
        private GameMode _gameMode;
        private GarbageFactory _garbageFactory;
        private bool _started;

        [Inject]
        private void Construct(GarbageFactory garbageFactory)
        {
            _garbageFactory = garbageFactory;
        }

        public void SetGameMode(GameMode gameMode)
        {
            _gameMode = gameMode;
        }

        public void Stop()
        {
            _started = false;
        }

        public IEnumerator GeneratorTask()
        {
            _started = true;
            
            int[] types = _gameMode.GetTankTypes();
            var difficultyTime = _gameMode.GetDifficultyTime(types.Length);
            float delay;
            float timeElapsed = 0f;

            while (_started)
            {
                _garbageFactory.Create(types);
                delay = Mathf.Lerp(
                    _gameMode.MinDelay,
                    _gameMode.MaxDelay,
                    1f - timeElapsed / difficultyTime);
                Debug.Log(delay);
                yield return new WaitForSeconds(delay);

                timeElapsed += delay;
                if (timeElapsed > difficultyTime)
                {
                    if (_gameMode.IsMaxLevel)
                    {
                        break;
                    }
                    
                    _gameMode.LevelUp();
                    timeElapsed = 0f;
                    types = _gameMode.GetTankTypes();
                    difficultyTime = _gameMode.GetDifficultyTime(types.Length);
                }
            }
            
            Debug.Log("Max Level");
            delay = _gameMode.MinDelay;
            var delayDiff = _gameMode.MaxLevelDelayDecrease;
            
            while (_started)
            {
                _garbageFactory.Create(types);
                yield return new WaitForSeconds(delay);
                delay -= delayDiff;
            }
        }
    }
}