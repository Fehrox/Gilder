using Application;
using Fluxor;

namespace Presentation.Store.Transaction
{

    public class TransactionsCsvLoadEffect : Effect<TransactionCsvLoadAction>
    {
        private readonly ITransactionImporter _transactionImporter;
        private readonly IRepo<Domain.Transaction> _transactionRepository;

        public TransactionsCsvLoadEffect(
            ITransactionImporter transactionImporter, 
            IRepo<Domain.Transaction> transactionRepository) 
        {
            _transactionImporter = transactionImporter;
            _transactionRepository = transactionRepository;
        }

        public override async Task HandleAsync(TransactionCsvLoadAction action, IDispatcher dispatcher)
        {
            var existingTransactions = await _transactionRepository.Read();
            var existingTransactionsList = existingTransactions.ToArray();

            var transactionSource = action.CsvText;
            var importedTransactions = await _transactionImporter.ImportTransactions(transactionSource);
            
            var addTransactions = new List<Domain.Transaction>();
            foreach (var transaction in importedTransactions) {
                var alreadyImported = existingTransactionsList
                    .Any(t => t.ToHash().ToString() == transaction.ToHash().ToString());
                if (alreadyImported) continue;

                transaction.Id = Guid.NewGuid();
                addTransactions.Add(transaction);
            }
            
            var importedTransaction = new TransactionCreateAction(addTransactions);
            dispatcher.Dispatch(importedTransaction);

            var repoLoadAction = new TransactionRepoCreateAction(addTransactions);
            dispatcher.Dispatch(repoLoadAction);
        }
    }
}