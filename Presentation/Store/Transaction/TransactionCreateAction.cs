namespace Presentation.Store.Transaction;

public record TransactionCreateAction (IEnumerable<Domain.Transaction> Transactions);

