using Preferences;
using UnityEngine;

namespace Services
{
    public class SettingsService : MonoBehaviour
    {
        public bool SoundEnabled
        {
            get => GamePreferences.IsSoundEnabled.Value;
            set => GamePreferences.IsSoundEnabled.Value = value;
        }
    }
}