using Common.UnitSystem;
using Common.UnitSystem.Stats;
using Generated;
using Plugins.Timer.Source;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base
{
    public class Base : Unit
    {
        [SerializeField]
        private BaseStatsManager _statsManager;

        [SerializeField] 
        private UnitSetup _unitSetup;
        
        public override UnitType UnitType => UnitType.Base;
        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _unitSetup;

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            _statsManager.Init();
            Armor = new UnitArmor(this, HealthFlag.Destructable | HealthFlag.Killable, _unitSetup);
            Armor.Died += OnDied;
            AddLifeCycleObjects(Armor);
        }

        private void OnDied(IUnit killedBy)
        {
            GameObject spawnedBaseExplosion = Instantiate(SpawnManager.Instance.GetSpawnPrefabForSpawnType(SpawnType.BaseExplosion), transform.position,
                Quaternion.identity);
            spawnedBaseExplosion.transform.localScale = transform.localScale;
        }
    }
}