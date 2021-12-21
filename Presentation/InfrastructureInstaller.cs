using Application;
using Gilder.Infrastructure;

namespace Presentation
{
    public static class InfrastructureInstaller
    {
        public static void InstallInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ITransactionImporter, NabCsvTransactionImporter>();
            services.AddSingleton<ITransactionRepository, InMemoryRepository>();
            services.AddScoped<ITransactionRepository, LocalStorageTransactionRepository>();
            services.AddSingleton<IGroupRepository, InMemoryRepository>();
        }
    }
}