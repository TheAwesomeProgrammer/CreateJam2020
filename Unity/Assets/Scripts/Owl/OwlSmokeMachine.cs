using System;
using Common.SpawnHanding;
using Common.UnitSystem;
using Common.UnitSystem.LifeCycle;
using Plugins.Timer.Source;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Owl
{
    public class OwlSmokeMachine
    {
        private Data _data;
        private OwlSetup _owlSetup;
        private bool _isSmokeMachineRunning;
        private Timer _spawnSmokeTimer;
        private Timer _growthCycleLoop;
        private float _smokeGrowthPerCycle;
        private WizardAnimation _wizardAnimation;
        
        public OwlSmokeMachine(OwlSetup owlSetup, WizardAnimation wizardAnimation, Data data)
        {
            _owlSetup = owlSetup;
            _data = data;
            _wizardAnimation = wizardAnimation;
            _smokeGrowthPerCycle = _data.SmokeGrowthPerSecond.Value / (1f / _data.SmokeGrowthCycleInterval.Value);
        }

        public void OnSmoke()
        {
            _isSmokeMachineRunning = !_isSmokeMachineRunning;
            _wizardAnimation.SetFireMode(_isSmokeMachineRunning);
            
            if (_isSmokeMachineRunning)
            {
                _growthCycleLoop?.Cancel();
                SpawnSmokeLoop();
            }
            else
            {
                _spawnSmokeTimer?.Cancel();
                GrowthCycleLoop();
            }
        }

        private void SpawnSmokeLoop()
        {
            if (_data.SmokeAmount.Value > 0)
            {
                GameObject spawnedSmoke = Object.Instantiate(SpawnManager.Instance.GetSpawnPrefabForSpawnType(SpawnType.Smoke),
                    _owlSetup.SmokeSpawnPoint.position, Quaternion.identity);
                Object.Destroy(spawnedSmoke, _data.LiveTime.Value);

                _data.SmokeAmount.DecreaseTempStat(_data.SmokeUsagePerCloud.Value);
                _spawnSmokeTimer = Timer.Register(_data.SpawnInterval.Value, SpawnSmokeLoop);
            }
        }

        private void GrowthCycleLoop()
        {
            _data.SmokeAmount.IncreaseTempStat(_smokeGrowthPerCycle);
            
            _growthCycleLoop = Timer.Register(_data.SmokeGrowthCycleInterval.Value, GrowthCycleLoop);
        }
        
        [Serializable]
        public class Data
        {
            public Stat SpawnInterval;
            public Stat LiveTime;
            public Stat SmokeUsagePerCloud;
            public Stat SmokeAmount;
            public Stat SmokeGrowthCycleInterval;
            public Stat SmokeGrowthPerSecond;
        }
    }
}