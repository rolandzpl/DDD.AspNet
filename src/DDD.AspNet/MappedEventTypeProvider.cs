using System;
using DDD.Domain;

namespace DDD
{
    class MappedEventTypeProvider : IEventTypeResolver
    {
        private EventTypeRegistry eventTypeRegistry;

        public MappedEventTypeProvider(EventTypeRegistry eventTypeRegistry)
        {
            this.eventTypeRegistry = eventTypeRegistry;
        }

        public Type GetEventType(string eventName)
        {
            return eventTypeRegistry.GetMappedType(eventName);
        }
    }
}