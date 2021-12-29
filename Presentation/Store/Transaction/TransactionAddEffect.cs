using Application;
using Fluxor;

namespace Presentation.Store
{
    public class TransactionCreateEffect : Effect<TransactionCreateAction>
    {
        private readonly ITransactionRepository _transactionRepo;

        public TransactionCreateEffect(ITransactionRepository localStorage) =>
            _transactionRepo = localStorage;

        public override async Task HandleAsync(TransactionCreateAction action, IDispatcher dispatcher) =>
            await _transactionRepo.Create(action.Transactions);
    }
}