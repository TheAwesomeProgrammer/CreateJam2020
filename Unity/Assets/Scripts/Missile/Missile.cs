using System;
using System.Collections.Generic;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using Plugins.Timer.Source;
using UnityEngine;

namespace Gameplay.Missile
{
    public class Missile : Unit, ISpawnedObject<Missile.Data>
    {
        private const float LIVE_TIME = 15f;
        
        [SerializeField]
        private MissileStatsManager _statsManager;

        [SerializeField] 
        private MovementSetup _movementSetup;
        
        public override UnitType UnitType => UnitType.Missile;
        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _movementSetup;

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            _statsManager.Init();
            Armor = new UnitArmor(this, HealthFlag.Destructable | HealthFlag.Killable, _movementSetup);
            AddLifeCycleObjects(Armor);
            Timer.Register(LIVE_TIME, () => Armor.Die());
        }

        public void OnSpawned(Data data)
        {
            MissleMovement missileMovement = new MissleMovement(_movementSetup, data, Armor);
            AddLifeCycleObject(missileMovement);
        }

        [Serializable]
        public class Data
        {
            public List<UnitType> UnitsToDamage;
            public Stat MovementSpeed;
            public Stat Damage;
            public Stat MinSpread;
            public Stat MaxSpread;
            
            public Vector2 MissileDirection { get; set; }
            public IUnit Owner { get; set; }
        }
    }
}