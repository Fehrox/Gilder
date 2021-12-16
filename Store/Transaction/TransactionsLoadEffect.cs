using System.Linq;
using System.Threading.Tasks;
using Fluxor;
using Reconciler.Application;

namespace Reconciler.Store
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
            var importedTransactions = await _transactionImporter.ImportTransactions();

            foreach (var transaction in importedTransactions) {
                var alreadyImported = existingTransactions
                    .Any(t => t.ToHash().ToString() == transaction.ToHash().ToString());
                if (alreadyImported) continue;

                transaction.Id = System.Guid.NewGuid();
                var importedTransaction = new TransactionCreateAction(transaction);
                dispatcher.Dispatch(importedTransaction);
            }

            foreach (var transaction in existingTransactions) {
                var restoredTransaction = new TransactionRestoreAction(transaction);
                dispatcher.Dispatch(restoredTransaction);
            }
        }
    }
}