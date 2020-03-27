using System.Collections.Generic;
using UnityEngine;

namespace Common.UnitSystem.Stats
{
    public abstract class UnitStatsManager<THealthStats> : ScriptableObject, IUnitStatsManager where THealthStats : UnitHealthStats
    {
        private List<object> _statsEntries;
        
        public abstract THealthStats HealthStats { get; }

        public virtual void Init()
        {
            _statsEntries = new List<object>();
            AddStats(HealthStats);
        }
        protected void AddStats(object statsObj)
        {
            _statsEntries.Add(statsObj);
        }

        public void Reset()
        {
            foreach (var statsEntry in _statsEntries)
            {
                if (statsEntry is IResetStats resetStats)
                {
                    resetStats.Reset();
                }
            }
        }

        public T GetStats<T>(bool logError = true) where T : class 
        {
            foreach (var statsEntry in _statsEntries)
            {
                if (statsEntry is T stats)
                {
                    return stats;
                }
            }

            if (logError)
            {
                Debug.LogError("Couldn't find type:  " + typeof(T));
            }
            
            return null;
        }

        public bool TryGetStats<T>(out T stats) where T : class
        {
            stats = GetStats<T>(false);
            return stats != null;
        }
    }
}