using System;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using Plugins.LeanTween.Framework;
using Plugins.Timer.Source;
using UnityEngine;

namespace Tower
{
    public class Tower : MovingUnit
    {
        [SerializeField]
        private EnemyStatsManager _statsManager;

        [SerializeField] 
        private TowerSetup _towerSetup;

        [SerializeField] 
        private TowerMissileLauncher.MissileLaunchData _missileLaunchData;

        [SerializeField] 
        private LayerMask _visionLayermask;

        [SerializeField] 
        private Transform _enemyVisionStartTransform;

        [SerializeField] 
        private OwlAnimation _owlAnimation;

        public override UnitType UnitType => UnitType.Tower;
        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _towerSetup;
        protected override UnitSlowManager SlowManager { get; set; }

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            _statsManager.Init();
            SlowManager = new UnitSlowManager(_statsManager.MovementStats);
            Armor = new UnitArmor(this, HealthFlag.Killable | HealthFlag.Destructable, _towerSetup);
            TowerVision towerVision = new TowerVision(_towerSetup, _enemyVisionStartTransform, _statsManager.EnemySpecificStats.EnemyVisionData, Vector2.up, Vector2.left, _visionLayermask);
            TowerMissileLauncher towerMissileLauncher = new TowerMissileLauncher(towerVision, _towerSetup,  _statsManager.UnitAttackStats, 
                _statsManager.EnemySpecificStats.MissileSpawnData,
                _missileLaunchData,
                this);
            TowerRotation towerRotation = new TowerRotation(_towerSetup, towerVision, Vector2.left);
            AddLifeCycleObjects(Armor, towerVision, towerMissileLauncher);
            towerMissileLauncher.LaunchedMissile += OnLaunchedMissile;
        }

        private void OnLaunchedMissile()
        {
            _owlAnimation.CannonShootAnimation();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            DebugExtension.DebugCone(_enemyVisionStartTransform.position, (_towerSetup.MovementTransform.rotation * Vector2.up) * 10, Color.green, 
                _statsManager.EnemySpecificStats.EnemyVisionData.ConeInDegrees.Value, 0, false);
        }
    }
}