using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameMode
    {
        public float MinSpeed => 1f;
        public float MaxSpeed => 2f;

        public TankEnableChangedEvent TankEnableChanged;
        public MaxTankChangedEvent MaxTankChanged;

        private readonly bool[] _tanksEnabled = new bool[6];
        private readonly float[] _difficultyTime = new float[6];

        private int _maxTank = -1;

        public GameMode()
        {
            for (int i = 0; i < _difficultyTime.Length; i++)
            {
                _difficultyTime[i] = (i + 1) * 60f / 6f;
            }
        }

        public void SetDifficulty(int index)
        {
            Debug.Log($"diff: {index}");
            var maxTank = index < 6 ? -1 : index;
            
            for (int i = 0; i < _tanksEnabled.Length; i++)
            {
                bool isEnabled = i <= index;
                if (_tanksEnabled[i] != isEnabled)
                {
                    _tanksEnabled[i] = isEnabled;
                    OnTankEnableChanged(i, isEnabled);
                }

                if (isEnabled && i > maxTank)
                {
                    maxTank = i;
                }
            }

            if (_maxTank != maxTank)
            {
                _maxTank = maxTank;
                OnMaxTankChanged(maxTank);
            }
        }

        public void SetTankEnabled(int type, bool value)
        {
            _tanksEnabled[type] = value;
            OnTankEnableChanged(type, value);
        }

        public int[] GetTankTypes()
        {
            var res = new List<int>();
            for (int i = 0; i < _tanksEnabled.Length; i++)
            {
                if (_tanksEnabled[i])
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