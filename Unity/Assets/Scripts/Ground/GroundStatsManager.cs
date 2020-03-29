using System;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable][CreateAssetMenu(fileName = "GroundStatsManager", menuName = "Stats/Ground stats manager", order = 52)]
    public class GroundStatsManager : UnitStatsManager<UnitHealthStats>
    {
        [SerializeField]
        private UnitHealthStats _healthStats;

        public override UnitHealthStats HealthStats => _healthStats;
    }
}