using System;

namespace Game
{
    public class TankEnableEventArgs : EventArgs
    {
        public int Type;
        public bool NewValue;

        public TankEnableEventArgs(int type, bool newValue)
        {
            Type = type;
            NewValue = newValue;
        }
    }
    
    public delegate void TankEnableChangedEvent(object sender, TankEnableEventArgs e);
}