using System;
using Common.UnitSystem;
using UnityEngine;

namespace Tower
{
    [Serializable]
    public class TowerSetup : MovementSetup
    {
        [SerializeField]
        private GameObject _triggerGo;

        [SerializeField] 
        private Transform _canon;

        public GameObject TriggerGo => _triggerGo;

        public Transform Canon => _canon;
    }
}