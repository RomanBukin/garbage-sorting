using System;
using UnityEngine;

namespace Preferences
{
    public abstract class Preference
    {
        public abstract void Init();
    }

    public class Preference<T> : Preference
    {
        protected readonly string Key;
        protected readonly T DefaultValue;
        protected T CachedValue;

        public T Value
        {
            get => CachedValue;
            set
            {
                CachedValue = value;
                SetValue(Key, value);
            }
        }

        public Preference(string key, T defaultValue)
        {
            if (typeof(T) != typeof(bool)
                && typeof(T) != typeof(int)
                && typeof(T) != typeof(float)
                && typeof(T) != typeof(string))
            {
                throw new ArgumentException("Unexpected type. Use bool, int, float or string.");
            }

            Key = key;
            DefaultValue = defaultValue;
        }

        public override void Init()
        {
            if (PlayerPrefs.HasKey(Key))
            {
                CachedValue = GetValue(Key);
            }
            else
            {
                Value = DefaultValue;
            }
        }

        protected static T GetValue(string key)
        {
            if (typeof(T) == typeof(bool))
            {
                return (T) (object) (PlayerPrefs.GetInt(key) != 0);
            }

            if (typeof(T) == typeof(int))
            {
                return (T) (object) PlayerPrefs.GetInt(key);
            }

            if (typeof(T) == typeof(float))
            {
                return (T) (object) PlayerPrefs.GetFloat(key);
            }

            return (T) (object) PlayerPrefs.GetString(key);
        }

        protected static void SetValue(string key, T value)
        {
            if (typeof(T) == typeof(bool))
            {
                PlayerPrefs.SetInt(key, (bool) (object) value ? 1 : 0);
            }
            else if (typeof(T) == typeof(int))
            {
                PlayerPrefs.SetInt(key, (int) (object) value);
            }
            else if (typeof(T) == typeof(float))
            {
                PlayerPrefs.SetFloat(key, (float) (object) value);
            }
            else
            {
                PlayerPrefs.SetString(key, (string) (object) value);
            }
        }
    }
}