using Application;
using Fluxor;

namespace Presentation.Store.Transaction;

public record TransactionUpdateAction(Domain.Transaction Transaction);

public class TransactionUpdateReducer : Reducer<TransactionState, TransactionUpdateAction>
{
    public override TransactionState Reduce(TransactionState state, TransactionUpdateAction action)
    {
        var updatedTransactions = new List<Domain.Transaction>();
        var transactions = state.Transactions;
        foreach (var transaction in transactions) {
            if (transaction.Id == action.Transaction.Id) {
                updatedTransactions.Add(action.Transaction);
            } else {
                updatedTransactions.Add(transaction);
            }
        }

        return state with {
            Transactions = updatedTransactions
        };
    }
}


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