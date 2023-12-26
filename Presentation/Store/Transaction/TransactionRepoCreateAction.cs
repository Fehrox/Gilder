using Application;
using Fluxor;

namespace Presentation.Store.Transaction;

public record TransactionRepoCreateAction(IEnumerable<Domain.Transaction> Transactions);

public class TransactionRepoCreateEffect : Effect<TransactionRepoCreateAction>
{
    private readonly IRepo<Domain.Transaction> _transactionRepo;

    public TransactionRepoCreateEffect(IRepo<Domain.Transaction> localStorage) 
        => _transactionRepo = localStorage;

    public override async Task HandleAsync(TransactionRepoCreateAction action, IDispatcher dispatcher) =>
        await _transactionRepo.Create(action.Transactions);
}