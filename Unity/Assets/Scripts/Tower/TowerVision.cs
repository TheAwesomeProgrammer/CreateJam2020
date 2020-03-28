using System;
using System.Collections.Generic;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using Common.Util;
using UnityEngine;
using Yurowm.DebugTools;

namespace Tower
{
    public delegate void TargetEnteredVision(IUnit target);
    public delegate void TargetStayedInVision(IUnit target);
    public delegate void TargetExitedVision(IUnit target);
    
    public class TowerVision : IUpdate
    {
        private Vector2 _visionLookDirection;
        private Vector2 _canonDirection;
        private Data _data;
        private IUnit _lastTarget;
        private LayerMask _visionLayermask;
        private Transform _towerVisionStartTransform;
        private List<IUnit> _unitsInVisionRange;
        private Transform _canon;

        public event TargetEnteredVision TargetEnteredVision;
        public event TargetEnteredVision TargetStayedInVision;
        public event TargetExitedVision TargetExitedVision;
        
        public TowerVision(TowerSetup towerSetup, Transform visionStartTransform, Data data, Vector2 visionLookDirection, Vector2 canonDirection, LayerMask visionLayermask)
        {
            _visionLayermask = visionLayermask;
            _towerVisionStartTransform = visionStartTransform;
            _canon = towerSetup.Canon;
            _data = data;
            _canonDirection = canonDirection;
            _visionLookDirection = visionLookDirection;
            _unitsInVisionRange = new List<IUnit>();
            AddTriggerNotifier(towerSetup.TriggerGo);
        }

        private void AddTriggerNotifier(GameObject triggerGo)
        {
            TriggerNotifier triggerNotifier = triggerGo.AddComponent<TriggerNotifier>();
            triggerNotifier.Init(new List<UnitType>(){ UnitType.Bomb, UnitType.Owl});
            triggerNotifier.UnitStayed += OnUnitStayed;
        }

        private void OnUnitStayed(UnitType unitType, IUnit unit)
        {
            _unitsInVisionRange.Add(unit);
        }
        
        public void Update()
        {
            IUnit closestUnitInRange = GetClosestVisibleUnitInRange();
            if (closestUnitInRange != null)
            {
                UpdateTargetStatus(closestUnitInRange);
            }
            _unitsInVisionRange.Clear();
        }

        private IUnit GetClosestVisibleUnitInRange()
        {
            float minDistanceToUnit = float.MaxValue;
            IUnit closestUnitInRange = null;
            
            foreach (var unit in _unitsInVisionRange)
            {
                MovementSetup movementSetup = unit.GetSetup<MovementSetup>();
                Vector2 unitPosition = movementSetup.MovementTransform.position;
                float distanceToUnit = Vector2.Distance(unitPosition, _towerVisionStartTransform.position);

                if (distanceToUnit < minDistanceToUnit && CanSeeTarget(unitPosition, unit))
                {
                    minDistanceToUnit = distanceToUnit;
                    closestUnitInRange = unit;
                }
            }

            return closestUnitInRange;
        }

        private void UpdateTargetStatus(IUnit target)
        {
            if (_lastTarget != target)
            {
                TargetExitedVision?.Invoke(_lastTarget);
                TargetEnteredVision?.Invoke(target);
            }

            if (_lastTarget == target)
            {
                TargetStayedInVision?.Invoke(target);
            }

            _lastTarget = target;
            DebugPanel.Log("CurrentTargetSeeing", "Tower", target);
        }

        private bool CanSeeTarget(Vector2 targetPosition, IUnit unit)
        {
            return IsPlayerWithinVisionCone(targetPosition) &&
                   IsNoObstructionToLineOfSightToTarget(targetPosition, unit);
        }

        private bool IsNoObstructionToLineOfSightToTarget(Vector2 targetPosition, IUnit unit)
        {
            Debug.DrawRay(_towerVisionStartTransform.position, DirectionToTarget(targetPosition) * int.MaxValue, Color.red);
            RaycastHit2D hit = Physics2D.Raycast(_towerVisionStartTransform.position, DirectionToTarget(targetPosition), int.MaxValue, _visionLayermask);
            return hit.transform != null && hit.transform.GetComponentInParent<IUnit>() == unit;
        }

        private bool IsPlayerWithinVisionCone(Vector2 targetPosition)
        {
            float degreeBetweenForwardDirectionAndPlayer = Vector2.Angle(VisionForwardDirection(), DirectionToTarget(targetPosition));
                
            return degreeBetweenForwardDirectionAndPlayer <= _data.ConeInDegrees.Value;
        }

        private Vector2 DirectionToTarget(Vector2 targetPosition)
        {
            return (targetPosition - (Vector2)_towerVisionStartTransform.position).normalized;
        }

        public Vector2 CanonForwardDirection()
        {
            return _canon.rotation * _canonDirection;
        }
        
        public Vector2 VisionForwardDirection()
        {
            return _visionLookDirection;
        }
        
        [Serializable]
        public class Data
        {
            public Stat ConeInDegrees;
        }
    }
}