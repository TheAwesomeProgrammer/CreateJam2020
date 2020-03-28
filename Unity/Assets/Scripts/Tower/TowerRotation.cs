﻿using Common.UnitSystem;
using UnityEngine;

namespace Tower
{
    public class TowerRotation
    {
        private Transform _canon;
        private Transform _movementTransform;
        private Vector2 _startLookingDirection;
        
        public TowerRotation(TowerSetup towerSetup, TowerVision towerVision, Vector2 startLookingDirection)
        {
            _startLookingDirection = startLookingDirection;
            towerVision.TargetStayedInVision += OnTargetEnteredVision;
            _canon = towerSetup.Canon;
            _movementTransform = towerSetup.MovementTransform;
        }

        private void OnTargetEnteredVision(IUnit target)
        {
            MovementSetup movementSetup = target.GetSetup<MovementSetup>();
            Vector2 unitPosition = movementSetup.MovementTransform.position;
            Vector2 directionToPlayer = (unitPosition - (Vector2)_movementTransform.position).normalized;
            float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            _canon.rotation = Quaternion.Euler(0f, 0f, angleToPlayer - 90);
        }
    }
}