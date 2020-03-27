using System.Collections;
using System.Collections.Generic;
using Common.UnitSystem;
using UnityEngine;

namespace Common.Util
{
    public delegate void UnitEntered(UnitType unitType, IUnit unit);
    public delegate void UnitStayed(UnitType unitType, IUnit unit);
    public delegate void UnitExited(UnitType unitType, IUnit unit);

    public class TriggerNotifier : MonoBehaviour
    {
        [SerializeField]
        private List<UnitType> _unitsToNotifyOn;

        public event UnitEntered UnitEntered;
        public event UnitStayed UnitStayed;
        public event UnitExited UnitExited;

        public void Init(List<UnitType> unitsToNotifyOn)
        {
            _unitsToNotifyOn = unitsToNotifyOn;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            IUnit otherUnit = GetUnitFromCollider(other);
            if (otherUnit != null && _unitsToNotifyOn.Contains(otherUnit.UnitType))
            {
                UnitEntered?.Invoke(otherUnit.UnitType, otherUnit);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            IUnit otherUnit = GetUnitFromCollider(other);
            if (otherUnit != null && _unitsToNotifyOn.Contains(otherUnit.UnitType))
            {
                UnitStayed?.Invoke(otherUnit.UnitType, otherUnit);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            IUnit otherUnit = GetUnitFromCollider(other);
            if (otherUnit != null && _unitsToNotifyOn.Contains(otherUnit.UnitType))
            {
                UnitExited?.Invoke(otherUnit.UnitType, otherUnit);
            }
        }

        private IUnit GetUnitFromCollider(Collider2D other)
        {
            IUnit otherUnit = other.GetComponent<IUnit>();
            if (otherUnit != null)
            {
                return otherUnit;
            }

            return other.GetComponentInParent<IUnit>();
        }
    }
}