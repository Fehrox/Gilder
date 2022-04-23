using Application;
using Domain;
using Infrastructure;

namespace Presentation
{
    public static class InfrastructureInstaller
    {
        public static void InstallInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ITransactionCsvImporter, NabCsvTransactionCsvImporter>();
            services.AddScoped<IRepo<Transaction>, LocalStorageTransactionRepo>();
            services.AddScoped<IRepo<Group>, LocalStorageGroupRepository>();
            services.AddScoped<IRepo<Budget>, LocalStorageBudgetRepository>();
        }
    }
}