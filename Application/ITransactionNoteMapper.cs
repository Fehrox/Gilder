using System.Threading.Tasks;
using Reconciler.Domain;

namespace Reconciler.Application
{
    public interface ITransactionNoteMapper
    {
        Task UpdateTransactionNote(Hash noteHash, string note);
        Task<string> GetNoteForTransaction(Hash transactionHash);
    }
}