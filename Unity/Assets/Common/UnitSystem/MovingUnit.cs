namespace Common.UnitSystem
{
    public abstract class MovingUnit<TConfig> : Unit where TConfig : UnitConfig
    {
        protected abstract TConfig Config { get; set; }
        protected abstract UnitSlowManager SlowManager { get; set; }

        public T GetConfig<T>() where T : UnitConfig
        {
            return ConvertObjectAndVerifyType<T>(Config);
        }

        public T GetSlowManager<T>() where T : UnitSlowManager
        {
            return ConvertObjectAndVerifyType<T>(SlowManager);
        }
    }
}