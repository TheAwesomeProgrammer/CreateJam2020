using System.Collections.Generic;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using Common.Util;
using UnityEngine;

namespace Gameplay.Missile
{
    public class MissleMovement : IUpdate
    {
        private MovementSetup _movementSetup;
        private Missile.Data _data;
        private Vector2 _missileDirection;
        private IUnit _owner;
        private IArmor _missileArmor;
        
        public MissleMovement(MovementSetup movementSetup, Missile.Data spawnData, IArmor missileArmor)
        {
            _movementSetup = movementSetup;
            _data = spawnData;
            _missileDirection = spawnData.MissileDirection;
            _owner = spawnData.Owner;
            _missileArmor = missileArmor;
            TriggerNotifier triggerNotifier = _movementSetup.MovementTransform.gameObject.AddComponent<TriggerNotifier>();
            triggerNotifier.Init(new List<UnitType>(){UnitType.All}, _owner);
            triggerNotifier.UnitEntered += OnUnitEntered;
        }

        private void OnUnitEntered(UnitType unitType, IUnit unit)
        {
            if (_data.UnitsToDamage.Contains(unitType))
            {
                unit.GetArmor<IArmor>().TakeDamage((int)_data.Damage.Value, _owner);
            }
            
            _missileArmor.Die(); 
        }

        public void Update()
        {
            _movementSetup.MovementTransform.Translate(_missileDirection * (_data.MovementSpeed.Value * Time.deltaTime), Space.World);
        }
    }
}