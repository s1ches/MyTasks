using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Notes.Application;

public static class DependecyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(
            configuration => configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
       
        return services;
    }
}