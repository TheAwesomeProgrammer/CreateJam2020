using System;
using System.Collections.Generic;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.Util;
using Plugins.Timer.Source;
using UnityEngine;

namespace Bomb
{
    public class Explosion : MonoBehaviour, ISpawnedObject<Explosion.Data>
    {
        private Data _data;
        
        [SerializeField]
        private GameObject _triggerGo;

        [SerializeField] 
        private Transform _rootTransform;

        public void OnSpawned(Data data)
        {
            _data = data;
            _rootTransform.localScale = Vector3.one * _data.Size;
            
            TriggerNotifier triggerNotifier = _triggerGo.AddComponent<TriggerNotifier>();
            triggerNotifier.Init(_data.UnitsToTarget);
            
            triggerNotifier.UnitEntered += OnUnitEntered;
            Timer.Register(data.LiveTime, () => Destroy(_rootTransform.gameObject));
        }

        private void OnUnitEntered(UnitType unitType, IUnit unit)
        {
            unit.GetArmor<IArmor>().TakeDamage(_data.Damage, _data.Owner);
        }
        
        public class Data
        {
            public int Damage { get; set; }
            public float Size { get; set; }
            public IUnit Owner { get; set; }

            public float LiveTime { get; set; }
            public List<UnitType> UnitsToTarget { get; set; }
        }


    }
}