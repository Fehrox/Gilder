using Application;
using Fluxor;

namespace Presentation.Store.Import;

public class ImportRepoCreateEffect : Effect<ImportRepoCreateAction>
{

    private readonly IRepo<Domain.Import> _importRepo;

    public ImportRepoCreateEffect(IRepo<Domain.Import> importRepo) => _importRepo = importRepo;

    public override async Task HandleAsync(ImportRepoCreateAction action, IDispatcher dispatcher) => 
        await _importRepo.Create(action.Imports);
}