using Application;
using Domain;
using Infrastructure;

namespace Presentation
{
    public static class InfrastructureInstaller
    {
        public static void InstallInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ITransactionImporter, NabCsvTransactionImporter>();
            services.AddScoped<IRepo<Transaction>, LocalStorageTransactionRepo>();
            services.AddScoped<IRepo<Group>, LocalStorageGroupRepository>();
        }
    }
}