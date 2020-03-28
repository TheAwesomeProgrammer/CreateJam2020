using System;
using System.Collections.Generic;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using Common.Util;
using UnityEngine;

namespace Bomb
{
    public class Bomb : MovingUnit, ISpawnedObject<Bomb.Data>
    {
        private Data _data;
        private bool _hasExplosionDied;

        [SerializeField]
        private BombStatsManager _statsManager;

        [SerializeField]
        private BombSetup _bombSetup;
        
        public override UnitType UnitType => UnitType.Bomb;
        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _bombSetup;
        protected override UnitSlowManager SlowManager { get; set; }

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            _statsManager.Init();
            Armor = new UnitArmor(this, HealthFlag.Killable | HealthFlag.Destructable, _bombSetup);
            SlowManager = new UnitSlowManager(_statsManager.MovementStats);
            AddLifeCycleObjects(Armor);
            Armor.Died += OnDied;
            SetupTrigger();
        }

        private void SetupTrigger()
        {
            TriggerNotifier triggerNotifier = _bombSetup.TriggerGo.AddComponent<TriggerNotifier>();
            triggerNotifier.Init(new List<UnitType>(){ UnitType.Tower, UnitType.Base, UnitType.Missile });
            triggerNotifier.UnitEntered += OnUnitEntered;
        }

        private void OnUnitEntered(UnitType unitType, IUnit unit)
        {
            Armor.Die();
        }

        public void OnSpawned(Data data)
        {
            _data = data;
        }
        
        private void OnDied(IUnit killedBy)
        {
            Explosion.Data explosionData = new Explosion.Data()
            {
                Damage = (int)_data.Damage.Value,
                Owner = _data.Owner, 
                Size = _data.ExplosionRadius.Value,
                LiveTime = _data.ExplosionLiveTime.Value,
                UnitsToTarget = new List<UnitType>(){ UnitType.Tower, UnitType.Base }
            };
            
            Spawner.Spawn(SpawnManager.Instance.GetSpawnPrefabForSpawnType<Explosion>(SpawnType.Explosion), 
                _bombSetup.MovementTransform.position, Vector3.zero,  explosionData);
        }

        [Serializable]
        public class Data
        {
            public Stat Damage;
            public Stat ExplosionRadius;
            public Stat Ammo;
            public Stat ExplosionLiveTime;

            public IUnit Owner;
        }
    }
}