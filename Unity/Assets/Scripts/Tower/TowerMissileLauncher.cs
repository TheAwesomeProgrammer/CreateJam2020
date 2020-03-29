using System;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using Common.UnitSystem.Stats;
using Gameplay.Missile;
using Plugins.Timer.Source;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tower
{
    public class TowerMissileLauncher : IOnDestroy
    {
        private UnitAttackStats _unitAttackStats;
        private bool _canAttack;
        private Timer _attackTimer;
        private Gameplay.Missile.Missile.Data _missileData;
        private IUnit _tower;
        private TowerVision _towerVision;
        private MissileLaunchData _missileLaunchData;
        private TowerSetup _movementSetup;

        public event Action LaunchedMissile;

        public TowerMissileLauncher(TowerVision towerVision, TowerSetup movementSetup, UnitAttackStats unitAttackStats,
            Gameplay.Missile.Missile.Data missileData, MissileLaunchData missileLaunchData, IUnit tower)
        {
            _towerVision = towerVision;
            _tower = tower;
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
            AudioManager.Instance.PlayCannonFireSound();
            _missileData.Owner = _tower;
            _missileData.MissileDirection = (_towerVision.CanonForwardDirection() * GetRandomSpread()).normalized;
            Spawner.Spawn(_missileLaunchData.MissilePrefab, _missileLaunchData.SpawnPoint.position,
                _movementSetup.Canon.eulerAngles, _missileData);
            _canAttack = false;
            _attackTimer = Timer.Register(_unitAttackStats.AttackSpeed, () => _canAttack = true);
            LaunchedMissile?.Invoke();
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