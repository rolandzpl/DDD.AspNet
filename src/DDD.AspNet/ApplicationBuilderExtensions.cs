using DDD.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DDD
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PublishEventsToMessageBus(this IApplicationBuilder app)
        {
            var eventStore = app.ApplicationServices.GetRequiredService<IEventStore>();
            var bus = app.ApplicationServices.GetRequiredService<IMessageBus>();
            eventStore.NewEvents += (s, e) =>
            {
                foreach (var _ in e.NewEvents)
                {
                    try
                    {
                        bus.Publish(_);
                    }
                    catch { }
                }
            };
            return app;
        }

        public static IApplicationBuilder ReplayAllEvents(this IApplicationBuilder app)
        {
            var eventStore = app.ApplicationServices.GetRequiredService<IEventStore>();
            var bus = app.ApplicationServices.GetRequiredService<IMessageBus>();
            eventStore.GetAllEvents();
            foreach (var eventId in eventStore.GetAllEvents())
            {
                try
                {
                    var e = eventStore.GetEventsById(eventId);
                    bus.Publish(e);
                }
                catch { }
            }
            return app;
        }
    }
}