using DentOnline.Application.Repositories.ClinicInformations;
using DentOnline.Application.Repositories.Treatments._Bases;
using DentOnline.Application.Repositories.Users;
using DentOnline.Persistence.Repositories.MongoDb.ClinicInformations;
using DentOnline.Persistence.Repositories.MongoDb.Treatments;
using DentOnline.Persistence.Repositories.MongoDb.Users;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DentOnline.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserReadRepository, MongoUserReadRepository>();
        services.AddScoped<IUserWriteRepository, MongoUserWriteRepository>();

        services.AddScoped<ITreatmentReadRepository, MongoTreatmentReadRepository>();
        services.AddScoped<ITreatmentWriteRepository, MongoTreatmentWriteRepository>();

        services.AddScoped<IClinicInformationReadRepository, MongoClinicInformationReadRepository>();
        services.AddScoped<IClinicInformationWriteRepository, MongoClinicInformationWriteRepository>();

        services.AddScoped(opt =>
        {
            return new MongoClient(
                "mongodb+srv://dentonline:Dentonline456!@dentonlinefirstcluster.ixeared.mongodb.net/?retryWrites=true&w=majority");
        });

        services.AddScoped<IClientSessionHandle>(opt =>
        {
            var session = opt.CreateScope().ServiceProvider.GetRequiredService<MongoClient>().StartSession();

            session.StartTransaction();

            return session;
        });

        return services;
    }
}