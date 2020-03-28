using System;
using Common.UnitSystem;
using UnityEngine;

namespace Owl
{
    [Serializable]
    public class OwlSetup : MovementSetup
    {
        [SerializeField]
        private Transform _bombSpawnPoint;

        [SerializeField] 
        private Transform _smokeSpawnPoint;

        public Transform BombSpawnPoint => _bombSpawnPoint;

        public Transform SmokeSpawnPoint => _smokeSpawnPoint;
    }
}