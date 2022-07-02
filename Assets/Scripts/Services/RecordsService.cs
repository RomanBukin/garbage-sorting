using System.Collections.Generic;
using Game;
using Preferences;
using UnityEngine;

namespace Services
{
    public class RecordsService : MonoBehaviour
    {
        private class RecordsContainer
        {
            public List<Record> RecordsClassic = new List<Record>();
            public List<Record> RecordsFirstMiss = new List<Record>();
        }

        private RecordsContainer _container;

        private void Awake()
        {
            _container = LoadRecords();
        }

        public int AddRecord(Record record, GameType type)
        {
            var records = type == GameType.Classic ? _container.RecordsClassic : _container.RecordsFirstMiss;
            
            records.Add(record);
            records.Sort((x, y) =>
            {
                var result = y.correct.CompareTo(x.correct);
                if (result == 0)
                {
                    // result = x.time.CompareTo(y.time);
                }

                return result;
            });
            SaveRecords();
            
            return records.IndexOf(record) + 1;
        }

        public List<Record> GetRecords(GameType type)
        {
            return type == GameType.Classic ? _container.RecordsClassic : _container.RecordsFirstMiss;
        }

        private void SaveRecords()
        {
            var json = JsonUtility.ToJson(_container);
            GamePreferences.Records.Value = json;
        }

        private RecordsContainer LoadRecords()
        {
            var json = GamePreferences.Records.Value;
            return string.IsNullOrEmpty(json) ? new RecordsContainer() : JsonUtility.FromJson<RecordsContainer>(json);
        } 
    }
}
