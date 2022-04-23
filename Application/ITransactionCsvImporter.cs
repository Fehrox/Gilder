using Domain;

namespace Application;

public interface ITransactionCsvImporter
{
    Task<IEnumerable<Transaction>> ImportTransactions(string transactionCsv, string colHeaders);
}