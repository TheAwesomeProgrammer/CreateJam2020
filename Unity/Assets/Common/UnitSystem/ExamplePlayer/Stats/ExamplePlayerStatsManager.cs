using System;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Common.UnitSystem.ExamplePlayer.Stats
{
    [Serializable][CreateAssetMenu(fileName = "ExamplePlayerStatsManager", menuName = "Example/player stats manager", order = 52)]
    public class ExamplePlayerStatsManager : UnitStatsManager<UnitHealthStats>
    {
        [SerializeField]
        private UnitHealthStats _healthStats;
        
        [SerializeField]
        private MovementStats _movementStats;

        public override UnitHealthStats HealthStats => _healthStats;

        public MovementStats MovementStats => _movementStats;

        public override void Init()
        {
            base.Init();
            AddStats(_healthStats);
            AddStats(_movementStats);
        }
    }
}