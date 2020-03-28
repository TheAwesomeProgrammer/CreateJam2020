using System;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using Common.UnitSystem.Stats;
using Plugins.Timer.Source;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tower
{
    public class EnemyMissileLauncher : IOnDestroy
    {
        private UnitAttackStats _unitAttackStats;
        private bool _canAttack;
        private Timer _attackTimer;
        private Gameplay.Missile.Missile.Data _missileData;
        private IUnit _enemy;
        private TowerVision _towerVision;
        private MissileLaunchData _missileLaunchData;
        private MovementSetup _movementSetup;

        public EnemyMissileLauncher(TowerVision towerVision, MovementSetup movementSetup, UnitAttackStats unitAttackStats,
            Gameplay.Missile.Missile.Data missileData, MissileLaunchData missileLaunchData, IUnit enemy)
        {
            _towerVision = towerVision;
            _enemy = enemy;
            _missileLaunchData = missileLaunchData;
            _movementSetup = movementSetup;
            towerVision.TargetEnteredVision += OnTargetSeen;
            towerVision.TargetStayedInVision += OnTargetSeen;
            _unitAttackStats = unitAttackStats;
            _missileData = missileData;
            _canAttack = true;
        }

        private void OnTargetSeen(IUnit target)
        {
            if (_canAttack)
            {
                LaunchMissile();
            }
        }

        private void LaunchMissile()
        {
            _missileData.Owner = _enemy;
            _missileData.MissileDirection = (_towerVision.CanonDirection() * GetRandomSpread()).normalized;
            Spawner.Spawn(_missileLaunchData.MissilePrefab, _missileLaunchData.SpawnPoint.position,
                _movementSetup.MovementTransform.eulerAngles, _missileData);
            _canAttack = false;
            _attackTimer = Timer.Register(_unitAttackStats.AttackSpeed, () => _canAttack = true);
        }

        private Vector2 GetRandomSpread()
        {
            float xSpread = Random.Range(_missileData.MinSpread.Value, _missileData.MaxSpread.Value);
            float ySpread = Random.Range(_missileData.MinSpread.Value, _missileData.MaxSpread.Value);
            
            return new Vector2(Random.Range(1 - xSpread, 1 + xSpread), Random.Range(1 - ySpread, 1 + ySpread));
        }

        public void OnDestroy()
        {
            _attackTimer?.Cancel();
        }

        [Serializable]
        public class  MissileLaunchData
        {
            public Gameplay.Missile.Missile MissilePrefab;
            public Transform SpawnPoint;
        }
    }
}