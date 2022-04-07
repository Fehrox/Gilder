using Application;
using Fluxor;

namespace Presentation.Store.Transaction
{
    public class TransactionUpdateEffect : Effect<TransactionUpdateAction>
    {
        private readonly IRepo<Domain.Transaction> _transactionRepository;

        public TransactionUpdateEffect(IRepo<Domain.Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public override async Task HandleAsync(TransactionUpdateAction action, IDispatcher dispatcher)
        {
            await _transactionRepository.Update(action.Transaction);
        }
    }
}