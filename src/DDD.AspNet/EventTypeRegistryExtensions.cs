namespace DDD
{
    public static class EventTypeRegistryExtensions
    {
        public static EventTypeRegistry Map<TEvent>(this EventTypeRegistry _this, string eventName)
        {
            return _this.Map(eventName, typeof(TEvent));
        }
    }
}