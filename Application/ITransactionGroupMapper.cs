using System.Threading.Tasks;
using Reconciler.Domain;

namespace Reconciler.Application
{
    public interface ITransactionGroupMapper
    {
        Task<Group> GetGroupForTransaction(Transaction transaction);
        Task MapTransactionToGroup(Transaction transaction, Group option);
    }
}