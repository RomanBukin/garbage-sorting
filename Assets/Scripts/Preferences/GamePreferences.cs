namespace Preferences
{
    public sealed class GamePreferences : PreferencesStorage
    {
        public static readonly Preference<bool> IsSoundEnabled = new Preference<bool>("IsSoundEnabled", true);

        static GamePreferences()
        {
            AddPreference(IsSoundEnabled);
            
            InitValues();
        } 
        
        private GamePreferences() {}
    }
}
