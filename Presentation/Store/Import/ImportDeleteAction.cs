using Application;
using Fluxor;
using Presentation.Store.Transaction;

namespace Presentation.Store.Import;

public record ImportDeleteAction(Domain.Import[] Imports);

public class ImportRepoDeleteReducer : Reducer<ImportState, ImportDeleteAction>
{
    public override ImportState Reduce(ImportState state, ImportDeleteAction action)
    {
        var updatedImports = state.Imports.Except(action.Imports);
        return new ImportState(Imports: updatedImports.ToList());
    }
}

public class ImportRepoDeleteEffect : Effect<ImportDeleteAction>
{
    private readonly IRepo<Domain.Import> _importRepo;

    public ImportRepoDeleteEffect(IRepo<Domain.Import> importRepo) => _importRepo = importRepo;

    public override async Task HandleAsync(ImportDeleteAction action, IDispatcher dispatcher)
    {
        var importedTransactions = action.Imports.SelectMany(i => i.TransactionIds);
        dispatcher.Dispatch(new TransactionDeleteAction(importedTransactions));
        await _importRepo.Delete(action.Imports);
    }
}