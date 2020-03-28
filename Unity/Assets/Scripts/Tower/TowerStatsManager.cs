using System;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Tower
{
    [Serializable][CreateAssetMenu(fileName = "TowerStatsManager", menuName = "Stats/Tower stats manager", order = 52)]
    public class EnemyStatsManager : UnitStatsManager<UnitHealthStats>
    {
        [SerializeField]
        private UnitHealthStats _unitHealthStats;
        
        [SerializeField]
        private MovementStats _movementStats;

        [SerializeField] 
        private UnitAttackStats _unitAttackStats;

        [SerializeField] 
        private EnemySpecificStats _enemySpecificStats;

        public override UnitHealthStats HealthStats => _unitHealthStats;

        public MovementStats MovementStats => _movementStats;

        public EnemySpecificStats EnemySpecificStats => _enemySpecificStats;

        public UnitAttackStats UnitAttackStats => _unitAttackStats;
    }
}