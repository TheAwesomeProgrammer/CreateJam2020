using System;
using UnityEngine;

namespace Common.UnitSystem.Stats
{
    [Serializable]
     public class UnitAttackStats: IResetStats
        {
            [SerializeField] 
            private Stat _attackSpeedStat;
            
            public float AttackSpeed => _attackSpeedStat.Value;
            
            public void IncreaseAttackSpeedRatio(float attackSpeedRatioIncrease)
            {
                _attackSpeedStat.DecreaseStat(attackSpeedRatioIncrease);
            }
    
            public virtual void Reset()
            {
                _attackSpeedStat.ResetTempStats();
            }
        }
}