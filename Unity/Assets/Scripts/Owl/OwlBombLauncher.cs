using Common.SpawnHanding;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using Common.UnitSystem.Stats;
using Plugins.Timer.Source;
using UnityEngine;

namespace Owl
{
    public class OwlBombLauncher : IOnDestroy
    {
        private OwlSetup _owlSetup;
        private UnitAttackStats _attackStats;
        private bool _canLaunchBomb;
        private Timer _attackTimer;
        private Bomb.Bomb.Data _bombData;
        private WizardAnimation _wizardAnimation;
        private BombCounter _bombCounter;

        public OwlBombLauncher(OwlSetup owlSetup, UnitAttackStats attackStats, Bomb.Bomb.Data bombData, WizardAnimation wizardAnimation, BombCounter bombCounter)
        {
            _owlSetup = owlSetup;
            _attackStats = attackStats;
            _bombData = bombData;
            _wizardAnimation = wizardAnimation;
            _bombCounter = bombCounter;
            _canLaunchBomb = true;
            _bombCounter.SetBombCount((int)_bombData.Ammo.Value);
        }

        public void OnLaunchBomb()
        {
            if (_canLaunchBomb && _bombData.Ammo.Value > 0)
            {
                AudioManager.Instance.PlaySpawnBombSound();
                AudioManager.Instance.PlayWooshSound();
                _wizardAnimation.WizardUseWandAnimation();
                _canLaunchBomb = false;
                Spawner.Spawn(SpawnManager.Instance.GetSpawnPrefabForSpawnType<Bomb.Bomb>(SpawnType.Bomb),
                    _owlSetup.BombSpawnPoint.position, Vector3.zero, _bombData);
                _attackTimer = Timer.Register(_attackStats.AttackSpeed, () => _canLaunchBomb = true);
                _bombData.Ammo.DecreaseTempStat(1);
                _bombCounter.SetBombCount((int)_bombData.Ammo.Value);
            }
        }

        public void OnDestroy()
        {
            _attackTimer.Cancel();
        }
    }
}