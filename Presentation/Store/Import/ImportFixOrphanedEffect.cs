using Domain;
using Fluxor;
using Presentation.Store.Transaction;

namespace Presentation.Store.Imports;

public class ImportFixOrphanedEffect : Effect<ImportFixOrphanedAction>
{
    private readonly IState<TransactionState> _transactionState;
    private readonly IState<ImportState> _importState;

    public ImportFixOrphanedEffect(
        IState<TransactionState> transactionState, 
        IState<ImportState> importState)
    {
        _transactionState = transactionState;
        _importState = importState;
    }

    public override Task HandleAsync(ImportFixOrphanedAction action, IDispatcher dispatcher)
    {
        var orphanedTransactions = FindOrphanedTransactions();
        var adoptingImport = AdoptOrphanedTransactions(orphanedTransactions);
        
        dispatcher.Dispatch(new ImportCreateAction(new []{adoptingImport}));
        dispatcher.Dispatch(new ImportRepoCreateAction(new[] {adoptingImport}));
        
        return Task.CompletedTask;
    }
    
    
    private Import AdoptOrphanedTransactions(HashSet<Domain.Transaction> orphanedTransactions)
    {
        var orphanedTransactionIds = orphanedTransactions
            .Select(t => t.Id);
        var timeOfCreation = DateTime.Now;
        var adoptingImport = new Import(timeOfCreation, orphanedTransactionIds);

        return adoptingImport;
    }

    private HashSet<Domain.Transaction> FindOrphanedTransactions()
    {
        var orphanedTransactions = new HashSet<Domain.Transaction>();

        foreach (var transaction in _transactionState.Value.Transactions)
        {
            bool isOrphan = true;

            foreach (var import in _importState.Value.Imports)
            {
                foreach (var importedTransactionId in import.TransactionIds)
                {
                    if (transaction.Id == importedTransactionId)
                    {
                        isOrphan = false;
                        break;
                    }
                }
            }
            
            if (isOrphan) 
                orphanedTransactions.Add(transaction);
        }

        return orphanedTransactions;
    }
}