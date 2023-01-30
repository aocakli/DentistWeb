using System.Reflection;
using DentOnline.Application.BehaviorPipelines.Validation;
using DentOnline.Application.Features.Users._Bases.BusinessRules;
using DentOnline.Application.Utilities.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace DentOnline.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(assembly);
        services.AddAutoMapper(assembly);
        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddScoped<LanguageService>();
        services.AddScoped<UserBusinessRules>();
        services.AddScoped<Random>();
        services.AddScoped<AuthService>();

        return services;
    }
}