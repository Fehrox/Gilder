using System.Threading.Tasks;
using Reconciler.Domain;

namespace Reconciler.Application
{
    public interface ITransactionGroupMapper
    {
        Task<Group> GetGroupForTransaction(Hash transactionHash);
        Task UpdateTransactionGroup(Hash transactionHash, Hash groupHash);
    }
}