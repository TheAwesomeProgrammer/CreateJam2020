using Common.Movement;
using Common.UnitSystem.ExamplePlayer.Stats;
using Common.UnitSystem.Stats;
using UnityEngine;
using Animator = Common._2DAnimation.Animator;


namespace Common.UnitSystem.ExamplePlayer
{
    public class ExamplePlayer : MovingUnit
    {
        private Animator _playerAnimator;
        private Movement.Movement _movement;
        private PlayerInputActions _playerInputActions;
        private MovementAnimation _movementAnimation;
        
        [SerializeField]
        private MovementSetup _movementSetup;

        [SerializeField] 
        private ExamplePlayerAnimatorData _examplePlayerAnimatorData;

        [SerializeField]
        private ExamplePlayerStatsManager _statsManager;

        public override UnitType UnitType => UnitType.Owl;

        public Animator PlayerAnimator => _playerAnimator;

        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override UnitSetup UnitSetup => _movementSetup;

        protected override IArmor Armor { get; set; }
        

        protected override UnitSlowManager SlowManager { get; set; }

        protected  void Start()
        {
            base.Awake();
            StatsManager.Init();
            _playerInputActions = new PlayerInputActions();
            SlowManager = new UnitSlowManager(GetStatsManager<ExamplePlayerStatsManager>().MovementStats);
            Armor = new UnitArmor(this, HealthFlag.Destructable | HealthFlag.Killable, _movementSetup);
            _playerAnimator = new Animator(this, _examplePlayerAnimatorData, _movementSetup);
            _movement = new PlayerMovement( _movementSetup, _statsManager.GetStats<MovementStats>());
            _movementAnimation = new MovementAnimation(_movementSetup, () => _movement.CurrentMoveDirection, 
                _statsManager.GetStats<MovementStats>(), _playerAnimator.AnimationStateManager);
            AddLifeCycleObjects(_playerAnimator, Armor, _movement, _movementAnimation);
        }
    }
}