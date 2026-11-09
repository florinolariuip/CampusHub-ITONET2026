using System.Reflection;

namespace CampusHub.Common.CQRS;

public static class HandlerRegistrationExtensions
{
    public static IServiceCollection AddHandlers(this IServiceCollection services, Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) || i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                .Select(i => new { HandlerType = t, InterfaceType = i }));

        foreach (var handler in handlerTypes)
        {
            services.AddTransient(handler.InterfaceType, handler.HandlerType);
        }

        return services;
    }
}