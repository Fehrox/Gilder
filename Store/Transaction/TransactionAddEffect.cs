using System.Threading.Tasks;
using Fluxor;
using Reconciler.Application;

namespace Reconciler.Store
{
    public class TransactionCreateEffect : Effect<TransactionCreateAction>
    {
        private ITransactionRepository _transactionRepo;

        public TransactionCreateEffect(ITransactionRepository localStorage) =>
            _transactionRepo = localStorage;

        public override async Task HandleAsync(TransactionCreateAction action, IDispatcher dispatcher) =>
            await _transactionRepo.Create(action.Transaction);
    }
}