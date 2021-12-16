using Fluxor;
using System.Threading.Tasks;
using Reconciler.Application;

namespace Reconciler.Store
{
    public class TransactionUpdateEffect : Effect<TransactionUpdateAction>
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionUpdateEffect(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public override async Task HandleAsync(TransactionUpdateAction action, IDispatcher dispatcher)
        {
            await _transactionRepository.Update(action.Transaction);
        }
    }
}