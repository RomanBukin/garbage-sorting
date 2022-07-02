using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameMode
    {
        public float MinDelay => 1f;
        public float MaxDelay => 2f;
        public float MaxLevelDelayDecrease => 0.001f;
        public int Difficulty => _difficulty;
        public GameType GameType => _difficulty == 6 ? GameType.FirstMiss : GameType.Classic;

        public int MistakeCount => _mistakeCount;
        public bool IsMaxLevel { get; private set; }

        public TankEnableChangedEvent TankEnableChanged;
        public MaxTankChangedEvent MaxTankChanged;

        private readonly bool[] _tanksEnabled = new bool[6];
        private readonly float[] _difficultyTime = new float[7];

        private int _maxTank = -1;
        private int _difficulty;
        private int _mistakeCount = 5;

        public GameMode()
        {
            for (int i = 0; i < _difficultyTime.Length; i++)
            {
                _difficultyTime[i] = (i + 1) * 60f;
            }
        }

        public void SetDifficulty(int index)
        {
            Debug.Log($"Difficulty: {index}");
            _difficulty = index;
            _mistakeCount = index == 6 ? 1 : 5;
            var maxTank = index < 6 ? -1 : index;
            
            for (int i = 0; i < _tanksEnabled.Length; i++)
            {
                bool isEnabled = i <= index;
                if (_tanksEnabled[i] != isEnabled)
                {
                    SetTankEnabled(i, isEnabled);
                }

                if (isEnabled && i > maxTank)
                {
                    maxTank = i;
                }
            }

            // Disable 3 random tanks.
            if (index == 6)
            {
                ISet<int> numbers = new HashSet<int>();
                while (numbers.Count < 3)
                {
                    numbers.Add(Random.Range(0, 6));
                }
                
                foreach (var number in numbers)
                {
                    _tanksEnabled[number] = false;
                }
            }

            if (_maxTank != maxTank)
            {
                _maxTank = maxTank;
                OnMaxTankChanged(maxTank);
            }

            IsMaxLevel = index == 5;
        }

        public void LevelUp()
        {
            if (_difficulty == 6)
            {
                var types = GetTankTypes(false);
                var typeIndex = Random.Range(0, types.Length);
                var type = types[typeIndex];
                
                SetTankEnabled(type, true);

                IsMaxLevel = types.Length == 1;
            }
            else
            {
                SetDifficulty(_difficulty + 1);
            }
        }

        private void SetTankEnabled(int type, bool value)
        {
            _tanksEnabled[type] = value;
            OnTankEnableChanged(type, value);
        }

        public int[] GetTankTypes(bool value = true)
        {
            var res = new List<int>();
            for (int i = 0; i < _tanksEnabled.Length; i++)
            {
                if (_tanksEnabled[i] == value)
                {
                    res.Add(i);
                }
            }
            return res.ToArray();
        }

        public float GetDifficultyTime(int index)
        {
            return _difficultyTime[index];
        }

        private void OnTankEnableChanged(int type, bool value)
        {
            var args = new TankEnableEventArgs(type, value);
            TankEnableChanged?.Invoke(this, args);
        }

        private void OnMaxTankChanged(int type)
        {
            var args = new MaxTankEventArgs(type);
            MaxTankChanged?.Invoke(this, args);
        }
    }
}