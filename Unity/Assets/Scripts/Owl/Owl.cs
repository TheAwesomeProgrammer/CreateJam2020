using System;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Owl
{
    public class Owl : MovingUnit, PlayerInputActions.IPlayerActions
    {
        private PlayerInputActions _playerInputActions;

        [SerializeField]
        private MovementSetup _movementSetup;
        
        public override UnitType UnitType => UnitType.Owl;
        protected override IUnitStatsManager StatsManager { get; }
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _movementSetup;
        protected override UnitSlowManager SlowManager { get; set; }

        protected override void Awake()
        {
            base.Awake();
            Armor = new UnitArmor(this, HealthFlag.Destructable | HealthFlag.Killable, _movementSetup);
        }

        private void SetupInput()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.SetCallbacks(this);
            _playerInputActions.Player.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnBomb(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnSmoke(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}