using System.Collections.Generic;
using UnityEngine;

namespace Preferences
{
    public class PreferencesStorage
    {
        private static readonly List<Preference> Preferences = new List<Preference>();

        protected static void InitValues()
        {
            Preferences.ForEach(pref => pref.Init());
        }

        public static void DefaultValues()
        {
            PlayerPrefs.DeleteAll();
            InitValues();
        }

        protected static void AddPreference(Preference pref)
        {
            Preferences.Add(pref);
        }
    }
}