using Microsoft.Extensions.DependencyInjection;
using Reconciler.Application;

namespace Reconciler.Infrastructure
{
    public static class InfrastructureInstaller
    {
        public static void InstallInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ITransactionRepository, NabCsvTransactionRepository>();
            services.AddSingleton<IGroupRepository, GroupRepository>();
            services.AddSingleton<ITransactionGroupMapper, TransactionGroupMapper>();
            services.AddSingleton<ITransactionNoteMapper, TransactionNoteMapper>();
        }
    }
}