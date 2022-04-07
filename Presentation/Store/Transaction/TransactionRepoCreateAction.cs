namespace Presentation.Store.Transaction;

public record TransactionRepoCreateAction(IEnumerable<Domain.Transaction> Transactions);