using Common.UnitSystem;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace DefaultNamespace
{
    public class Ground : Unit
    {
        [SerializeField]
        private GroundStatsManager _statsManager;

        [SerializeField] 
        private UnitSetup _unitSetup;
        
        public override UnitType UnitType => UnitType.Ground;
        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _unitSetup;

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            _statsManager.Init();
            Armor = new UnitArmor(this, HealthFlag.None, _unitSetup);
            AddLifeCycleObjects(Armor);
        }
    }
}