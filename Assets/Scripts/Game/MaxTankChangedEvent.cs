using System;

namespace Game
{
    public class MaxTankEventArgs : EventArgs
    {
        public int Type;

        public MaxTankEventArgs(int type)
        {
            Type = type;
        }
    }
    
    public delegate void MaxTankChangedEvent(object sender, MaxTankEventArgs e);
}