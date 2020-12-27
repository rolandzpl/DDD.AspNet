using System;
using System.Collections.Generic;

namespace DDD
{
    public class EventTypeRegistry
    {
        private readonly Dictionary<string, Type> mappedEvents = new Dictionary<string, Type>();

        public EventTypeRegistry Map(string eventName, Type type)
        {
            mappedEvents.Add(eventName, type);
            return this;
        }

        public Type GetMappedType(string eventName)
        {
            Type result = null;
            return mappedEvents.TryGetValue(eventName, out result)
                ? result
                : null;
        }
    }
}