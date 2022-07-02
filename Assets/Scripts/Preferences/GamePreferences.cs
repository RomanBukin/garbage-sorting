namespace Preferences
{
    public sealed class GamePreferences : PreferencesStorage
    {
        public static readonly Preference<bool> IsSoundEnabled = new Preference<bool>("IsSoundEnabled", true);
        public static readonly Preference<string> Records = new Preference<string>("Records", "");

        static GamePreferences()
        {
            AddPreference(IsSoundEnabled);
            AddPreference(Records);
            
            InitValues();
        } 
        
        private GamePreferences() {}
    }
}
