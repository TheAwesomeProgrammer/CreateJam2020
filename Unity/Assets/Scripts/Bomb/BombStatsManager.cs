using System;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Bomb
{
    [Serializable][CreateAssetMenu(fileName = "BombStatsManager", menuName = "Stats/Bomb stats manager", order = 52)]
    public class BombStatsManager : UnitStatsManager<UnitHealthStats>
    {
        [SerializeField]
        private UnitHealthStats _healthStats;
        
        [SerializeField]
        private MovementStats _movementStats;

        public override UnitHealthStats HealthStats => _healthStats;
        public MovementStats MovementStats => _movementStats;
    }
}