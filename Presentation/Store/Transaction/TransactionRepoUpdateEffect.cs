using Application;
using Fluxor;

namespace Presentation.Store.Transaction
{
    public class TransactionRepoUpdateEffect : Effect<TransactionUpdateAction>
    {
        private readonly IRepo<Domain.Transaction> _transactionRepository;

        public TransactionRepoUpdateEffect(IRepo<Domain.Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public override async Task HandleAsync(TransactionUpdateAction action, IDispatcher dispatcher)
        {
            await _transactionRepository.Update(action.Transaction);
        }
    }
}