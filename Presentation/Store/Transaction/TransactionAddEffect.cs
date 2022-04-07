using Application;
using Fluxor;

namespace Presentation.Store.Transaction
{
    public class TransactionCreateEffect : Effect<TransactionCreateAction>
    {
        private readonly IRepo<Domain.Transaction> _transactionRepo;

        public TransactionCreateEffect(IRepo<Domain.Transaction> localStorage) =>
            _transactionRepo = localStorage;

        public override async Task HandleAsync(TransactionCreateAction action, IDispatcher dispatcher) =>
            await _transactionRepo.Create(action.Transactions);
    }
}