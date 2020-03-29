using Common.UnitSystem;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace DefaultNamespace
{
    public class Structure : Unit
    {
        [SerializeField]
        private GroundStatsManager _statsManager;

        [SerializeField] 
        private UnitSetup _unitSetup;

        [SerializeField] 
        private HealthFlag _healthFlag;

        [SerializeField] 
        private UnitType _unitType;

        public override UnitType UnitType => _unitType;
        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _unitSetup;

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            _statsManager.Init();
            Armor = new UnitArmor(this, _healthFlag, _unitSetup);
            AddLifeCycleObjects(Armor);
        }
    }
}