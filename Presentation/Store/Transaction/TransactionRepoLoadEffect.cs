using Application;
using Fluxor;
using Presentation.Store.Group;

namespace Presentation.Store.Transaction;

public class TransactionRepoLoadEffect : Effect<TransactionRepoLoadAction>
{
    private readonly IRepo<Domain.Transaction> _transactionRepo;

    public TransactionRepoLoadEffect(IRepo<Domain.Transaction> transactionRepo)
    {
        _transactionRepo = transactionRepo;
    }

    public override async Task HandleAsync(TransactionRepoLoadAction action, IDispatcher dispatcher)
    {
        var transactions = await _transactionRepo.Read();
        dispatcher.Dispatch(new TransactionCreateAction(transactions));
    }
}