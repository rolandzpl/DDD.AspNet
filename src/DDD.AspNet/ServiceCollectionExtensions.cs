using System;
using DDD.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace DDD
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventTypeResolver(
            this IServiceCollection services,
            Action<EventTypeRegistry> configure)
        {
            services.AddSingleton<IEventTypeResolver>(serviceProvider =>
            {
                var eventTypeRegistry = new EventTypeRegistry();
                configure(eventTypeRegistry);
                return new MappedEventTypeProvider(eventTypeRegistry);
            });
            return services;
        }

        public static IServiceCollection AddFileEventStore(
            this IServiceCollection services,
            Action<FileEventStoreOptions> configure)
        {
            services.AddSingleton<IEventStore>(serviceProvider =>
            {
                var options = new FileEventStoreOptions()
                {
                    EventSerializer = new JsonEventSerializer(),
                    FileSystem = new FileSystem(),
                    RootDirectory = ".",
                };
                configure(options);
                if (options.EventTypeResolver == null)
                {
                    options.EventTypeResolver = serviceProvider.GetRequiredService<IEventTypeResolver>();
                }
                return new FileEventStore(
                    options.RootDirectory,
                    options.FileSystem,
                    options.EventSerializer,
                    options.EventTypeResolver);
            });
            return services;
        }
    }
}