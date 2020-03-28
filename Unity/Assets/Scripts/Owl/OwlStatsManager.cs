using System;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Owl
{    
    [Serializable][CreateAssetMenu(fileName = "OwlStatsManager", menuName = "Stats/Owl stats manager", order = 52)]
    public class OwlStatsManager : UnitStatsManager<UnitHealthStats>
    {
        [SerializeField]
        private UnitHealthStats _healthStats;

        [SerializeField] 
        private MovementStats _movementStats;

        [SerializeField] 
        private UnitAttackStats _attackStats;

        [SerializeField]
        private Bomb.Bomb.Data _bombStats;

        [SerializeField] 
        private OwlSmokeMachine.Data _owlSmokeStats;

        public override UnitHealthStats HealthStats => _healthStats;

        public MovementStats MovementStats => _movementStats;

        public UnitAttackStats AttackStats => _attackStats;

        public Bomb.Bomb.Data BombStats => _bombStats;

        public OwlSmokeMachine.Data OwlSmokeStats => _owlSmokeStats;
    }
}