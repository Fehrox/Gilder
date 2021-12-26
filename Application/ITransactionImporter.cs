using Domain;

namespace Application
{
    public interface ITransactionImporter
    {
        Task<IEnumerable<Transaction>> ImportTransactions(string transactionCsv);
    }

    public interface ITransactionSource
    {
        Task<string> ReadTransactionText();
    }
}