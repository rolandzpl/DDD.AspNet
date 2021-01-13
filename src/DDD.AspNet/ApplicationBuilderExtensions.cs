using System;
using System.Diagnostics;
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
                        Trace.WriteLine($"Publishing new event {_.GetType().Name} to message bus");
                        bus.Publish(_);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine($"Error occured while publishing event: {ex}");
                    }
                }
            };
            return app;
        }

        public static IApplicationBuilder ReplayAllEvents(this IApplicationBuilder app)
        {
            var eventStore = app.ApplicationServices.GetRequiredService<IEventStore>();
            var bus = app.ApplicationServices.GetRequiredService<IMessageBus>();
            Trace.WriteLine($"Preparing to replay all events to message bus");
            foreach (var eventId in eventStore.GetAllEvents())
            {
                try
                {
                    Trace.WriteLine($"Replaying event {eventId}");
                    var e = eventStore.GetEvent(eventId);
                    Trace.WriteLine($"Publishing event of type {e.GetType().Name}");
                    bus.Publish(e);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"Error occured while replying event: {ex}");
                }
            }
            return app;
        }
    }
}