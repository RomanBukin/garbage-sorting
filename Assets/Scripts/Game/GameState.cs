using System;

namespace Game
{
    public class GameState
    {
        public event EventHandler<int> CorrectChanged; 
        public event EventHandler<int> IncorrectChanged; 
        public event EventHandler<int> MissedChanged;

        public DateTime StartTime { get; } = DateTime.Now;
        public DateTime EndTime { get; private set; }
        public TimeSpan Time => EndTime - StartTime;
        
        public int Correct { get; private set; }
        public int Incorrect { get; private set; }
        public int Missed { get; private set; }

        public void Stop()
        {
            EndTime = DateTime.Now;
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

        public Record MakeRecord(GameType type)
        {
            return new Record
            {
                Type = type,
                Time = this.Time,
                Correct = this.Correct,
                Incorrect = this.Incorrect,
                Missed = this.Missed
            };
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