using System;
using Common.Movement;
using Common.UnitSystem;
using Common.UnitSystem.Stats;
using Common.Util;
using Plugins.LeanTween.Framework;
using Plugins.Timer.Source;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Owl
{
    public class Owl : MovingUnit, PlayerInputActions.IPlayerActions
    {
        private PlayerInputActions _playerInputActions;
        private PlayerMovement _playerMovement;
        private OwlBombLauncher _owlBombLauncher;
        private OwlSmokeMachine _owlSmokeMachine;
        private Color _originalColor;

        [SerializeField]
        private OwlSetup _owlSetup;

        [SerializeField] 
        private OwlStatsManager _statsManager;

        [SerializeField] 
        private WizardAnimation _wizardAnimation;

        [SerializeField] 
        private Color _damageColor;

        [SerializeField] 
        private float _damageTime;

        [SerializeField] 
        private float _damageFadeInOutTime;

        public override UnitType UnitType => UnitType.Owl;
        protected override IUnitStatsManager StatsManager => _statsManager;
        protected override IArmor Armor { get; set; }
        protected override UnitSetup UnitSetup => _owlSetup;
        protected override UnitSlowManager SlowManager { get; set; }

        protected override void Awake()
        {
            base.Awake();
            _statsManager = Instantiate(_statsManager);
            _statsManager.Init();
            _originalColor = Color.white;
            Armor = new UnitArmor(this, HealthFlag.Destructable | HealthFlag.Killable, _owlSetup);
            _playerMovement = new PlayerMovement(_owlSetup, _statsManager.MovementStats);
            _owlBombLauncher = new OwlBombLauncher(_owlSetup, _statsManager.AttackStats, _statsManager.BombStats, _wizardAnimation, MyGameManager.Instance.BombCounter);
            _owlSmokeMachine = new OwlSmokeMachine(_owlSetup, _wizardAnimation, _statsManager.OwlSmokeStats);
            Bar bar = new Bar(MyGameManager.Instance.SmokeBarTransform, () => _owlSmokeMachine.SmokeBarProcent);
            SetupInput();
            AddLifeCycleObjects( Armor, _playerMovement, bar, _owlSmokeMachine);
            Armor.Died += OnDied;
            Armor.TookDamage += OnTookDamage;
        }

        private void OnTookDamage(int damage, IUnit unitdealingdamage)
        {
            foreach (var spriteRenderer in _owlSetup.RootGo.GetComponentsInChildren<SpriteRenderer>())
            {
                LeanTween.color(spriteRenderer.gameObject, _damageColor, _damageFadeInOutTime);
                Timer.Register(_damageTime, () => ResetColor(spriteRenderer));
            }
            AudioManager.Instance.PlayOwlHitSound();
            MyGameManager.Instance.HealthUiScript.SetHealth((int)Armor.Health);
        }

        private void ResetColor(SpriteRenderer spriteRenderer)
        {
            if (spriteRenderer != null)
            {
                LeanTween.color(spriteRenderer.gameObject, _originalColor, _damageFadeInOutTime);
            }
            
        }

        private void OnEnable()
        {
            SetupInput();
        }

        private void OnDied(IUnit killedBy)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void SetupInput()
        {
            if (_playerInputActions == null)
            {
                _playerInputActions = new PlayerInputActions();
                _playerInputActions.Player.SetCallbacks(this);
            }
 
            _playerInputActions.Player.Enable();
        }
        
        public void OnDisable()
        {
            _playerInputActions.Player.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _playerMovement.OnMove(context.ReadValue<Vector2>());
        }

        public void OnBomb(InputAction.CallbackContext context)
        {
            _owlBombLauncher.OnLaunchBomb();
        }

        public void OnSmoke(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _owlSmokeMachine.OnSmoke();
            }
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}