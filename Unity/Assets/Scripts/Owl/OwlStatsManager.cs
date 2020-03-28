using System;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Owl
{
    public class OwlStatsManager : UnitStatsManager<UnitHealthStats>
    {
        [SerializeField]
        private UnitHealthStats _healthStats;

        public override UnitHealthStats HealthStats => _healthStats;
    }
}