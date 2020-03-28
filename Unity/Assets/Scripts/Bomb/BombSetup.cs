using System;
using Common.UnitSystem;
using UnityEngine;

namespace Bomb
{
    [Serializable]
    public class BombSetup : MovementSetup
    {
        [SerializeField]
        private GameObject _triggerGo;

        public GameObject TriggerGo => _triggerGo;
    }
}