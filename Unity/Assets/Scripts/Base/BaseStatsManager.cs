using System;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Base
{
    [Serializable][CreateAssetMenu(fileName = "BaseStatsManager", menuName = "Stats/Base stats manager", order = 52)]
    public class BaseStatsManager : UnitStatsManager<UnitHealthStats>
    {
        [SerializeField]
        private UnitHealthStats _healthStats;

        public override UnitHealthStats HealthStats => _healthStats;
    }
}