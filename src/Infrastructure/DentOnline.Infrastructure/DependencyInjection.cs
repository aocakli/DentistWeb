using DentOnline.Application.Features.Notifications.Abstracts;
using DentOnline.Infrastructure.Options;
using DentOnline.Infrastructure.Services.Notifications.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DentOnline.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        services.AddScoped<INotificationService, EmailNotificationService>();

        var configuration = services.BuildServiceProvider().CreateScope().ServiceProvider.GetService<IConfiguration>();

        services.Configure<EmailSettingOption>(configuration.GetSection("EmailSettings"));

        return services;
    }
}