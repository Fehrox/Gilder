using System.Diagnostics;
using Application;
using Domain;
using Fluxor;

namespace Presentation.Store
{

    public class TransactionsLoadEffect : Effect<TransactionsLoadAction>
    {
        private readonly ITransactionImporter _transactionImporter;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsLoadEffect(
            ITransactionImporter transactionImporter, 
            ITransactionRepository transactionRepository) 
        {
            _transactionImporter = transactionImporter;
            _transactionRepository = transactionRepository;
        }

        public override async Task HandleAsync(TransactionsLoadAction action, IDispatcher dispatcher)
        {
            var existingTransactions = await _transactionRepository.ReadTransactions();
            var existingTransactionsList = existingTransactions.ToArray();

            var transactionSource = action.CsvText;
            var importedTransactions = await _transactionImporter.ImportTransactions(transactionSource);
            
            var addTransactions = new List<Transaction>();
            foreach (var transaction in importedTransactions) {
                var alreadyImported = existingTransactionsList
                    .Any(t => t.ToHash().ToString() == transaction.ToHash().ToString());
                if (alreadyImported) continue;

                transaction.Id = Guid.NewGuid();
                addTransactions.Add(transaction);
            }
            
            var importedTransaction = new TransactionCreateAction(addTransactions);
            dispatcher.Dispatch(importedTransaction);

            var restoredTransaction = new TransactionRestoreAction(existingTransactionsList);
            dispatcher.Dispatch(restoredTransaction);
        }
    }
}