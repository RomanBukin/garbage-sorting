using System;

namespace Game
{
    public class GameState
    {
        public event EventHandler<int> CorrectChanged; 
        public event EventHandler<int> IncorrectChanged; 
        public event EventHandler<int> MissedChanged;

        public DateTime StartTime { get; } = DateTime.Now;

        private DateTime _endTime;
        public int Correct { get; private set; }
        public int Incorrect { get; private set; }
        public int Missed { get; private set; }

        public void Stop()
        {
            _endTime = DateTime.Now;
        }

        public void IncrementCorrect()
        {
            Correct++;
            OnCorrectChanged();
        }
        
        public void IncrementIncorrect()
        {
            Incorrect++;
            OnIncorrectChanged();
        }
        
        public void IncrementMissed()
        {
            Missed++;
            OnMissedChanged();
        }

        private void OnCorrectChanged()
        {
            CorrectChanged?.Invoke(this, Correct);
        }
        
        private void OnIncorrectChanged()
        {
            IncorrectChanged?.Invoke(this, Incorrect);
        }
        
        private void OnMissedChanged()
        {
            MissedChanged?.Invoke(this, Missed);
        }
    }
}