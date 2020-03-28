using System;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Gameplay.Missile
{
    [Serializable][CreateAssetMenu(fileName = "MissileStatsManager", menuName = "Stats/Missile stats manager", order = 54)]
    public class MissileStatsManager : UnitStatsManager<UnitHealthStats>
    {
        [SerializeField]
        private UnitHealthStats _unitHealthStats;

        public override UnitHealthStats HealthStats => _unitHealthStats;
    }
}