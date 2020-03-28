using Common.UnitSystem;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Bomb
{
    public class Bomb : MovingUnit
    {
        [SerializeField]
        private BombStatsManager _statsManager;

        [SerializeField]
        private MovementSetup _movementSetup;
        
        public override UnitType UnitType => UnitType.Bomb;
        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _movementSetup;
        protected override UnitSlowManager SlowManager { get; set; }

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            _statsManager.Init();
            Armor = new UnitArmor(this, HealthFlag.Killable | HealthFlag.Destructable, _movementSetup);
            SlowManager = new UnitSlowManager(_statsManager.MovementStats);
            AddLifeCycleObjects(Armor);
        }
    }
}