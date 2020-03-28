using System;
using UnityEngine;

namespace Tower
{
    [Serializable]
    public class EnemySpecificStats
    {
        [SerializeField]
        private TowerVision.Data _enemyVisionData;

        [SerializeField] 
        private Gameplay.Missile.Missile.Data _missileSpawnData;

        public TowerVision.Data EnemyVisionData => _enemyVisionData;

        public Gameplay.Missile.Missile.Data MissileSpawnData => _missileSpawnData;
    }
}