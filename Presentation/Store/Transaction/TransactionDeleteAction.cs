using Application;
using Fluxor;

namespace Presentation.Store.Transaction;

public record TransactionDeleteAction(IEnumerable<Guid> TransactionIds);

public class TransactionDeleteReducer : Reducer<TransactionState, TransactionDeleteAction>
{
    public override TransactionState Reduce(TransactionState state, TransactionDeleteAction action)
    {
        return state with {
            Transactions = state.Transactions
                .Where(t => !action.TransactionIds.Contains(t.Id))
        };
    }
}

public class TransactionDeleteEffect : Effect<TransactionDeleteAction>
{
    private readonly IRepo<Domain.Transaction> _transactionRepository;

    public TransactionDeleteEffect(IRepo<Domain.Transaction> transactionRepository) =>
        _transactionRepository = transactionRepository;

    public override async Task HandleAsync(TransactionDeleteAction deleteAction, IDispatcher dispatcher) => 
        await _transactionRepository.Delete(deleteAction.TransactionIds.ToArray());
}