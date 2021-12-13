using Microsoft.Extensions.DependencyInjection;
using Reconciler.Application;

namespace Reconciler.Infrastructure
{
    public static class InfrastructureInstaller
    {
        public static void InstallInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ITransactionImporter, NabCsvTransactionImporter>();
            services.AddSingleton<ITransactionRepository, InMemoryTransactionRepository>();
            services.AddSingleton<IGroupRepository, InMemoryTransactionRepository>();
        }
    }
}