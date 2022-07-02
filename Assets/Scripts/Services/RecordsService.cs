using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Services
{
    public class RecordsService : MonoBehaviour
    {
        private readonly List<Record> _recordsClassic = new List<Record>();
        private readonly List<Record> _recordsFirstMiss = new List<Record>();

        public int AddRecord(Record record)
        {
            var records = record.Type == GameType.Classic
                ? _recordsClassic
                : _recordsFirstMiss;
            
            records.Add(record);
            records.Sort((x, y) =>
            {
                var result = x.Correct.CompareTo(y.Correct);
                if (result == 0)
                {
                    result = x.Time.CompareTo(y.Time);
                }

                return result;
            });
            // TODO save records
            
            return records.IndexOf(record) + 1;
        }
    }
}
