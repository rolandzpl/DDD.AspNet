using Microsoft.AspNetCore.Builder;

namespace DDD
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ReplayAllEvents(this IApplicationBuilder app)
        {
            return app;
        }
    }
}