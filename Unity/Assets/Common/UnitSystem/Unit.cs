using System;
using Common.UnitSystem.LifeCycle;
using Common.UnitSystem.Stats;
using UnityEngine;

namespace Common.UnitSystem
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        private LifeCycleHandler _lifeCycleHandler;
        
        public abstract UnitType UnitType { get; }
        protected abstract IUnitStatsManager StatsManager { get; }
        
        protected abstract IArmor Armor { get; set; }
        
        protected abstract UnitSetup UnitSetup { get; }

        public T GetArmor<T>() where T : IArmor
        {
            return ConvertObjectAndVerifyType<T>(Armor);
        }
        
        public T GetSetup<T>() where T : UnitSetup
        {
            return ConvertObjectAndVerifyType<T>(UnitSetup);
        }

        public T GetStatsManager<T>() where T : IUnitStatsManager
        {
            return ConvertObjectAndVerifyType<T>(StatsManager);
        }
        
        protected T ConvertObjectAndVerifyType<T>(object obj) 
        {
            Type wantedConfigType = typeof(T);
            
            if (VerifyObjectType(obj, typeof(T)))
            {
                return (T) obj;
            }

            Debug.LogError(
                $"Tried to get incorrect obj: {obj.GetType().Name} wantedObjectType: {wantedConfigType} ");
            return default(T);
        }

        protected bool VerifyObjectType(object obj, Type wantedObjectType)
        {
            Type objType = obj.GetType();
            return wantedObjectType.IsInstanceOfType(obj) || objType == wantedObjectType;
        }
        
        protected void AddLifeCycleObject(object obj)
        {
            _lifeCycleHandler.AddLifeCycleObject(obj);
        }
        
        protected void AddLifeCycleObjects(params object[] objs)
        {
            _lifeCycleHandler.AddLifeCycleObjects(objs);
        }
        
        protected virtual void Awake()
        {
            _lifeCycleHandler = new LifeCycleHandler();
        }

        private void Update()
        {
            _lifeCycleHandler.Update();
        }
        
        private void FixedUpdate()
        {
            _lifeCycleHandler.FixedUpdate();
        }
        
        private void OnDestroy()
        {
            _lifeCycleHandler?.OnDestroy();
        }

        protected virtual void OnDrawGizmos()
        {
            _lifeCycleHandler?.OnDrawGizmos();
        }
        
    }
}