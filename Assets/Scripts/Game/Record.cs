using System;

namespace Game
{
    [Serializable]
    public class Record
    {
        // ReSharper disable once InconsistentNaming
        public TimeSpan time;
        public int correct;
        public int incorrect;
        public int missed;
    }
}