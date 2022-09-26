using Application;
using Domain;
using Fluxor;

namespace Presentation.Store.Imports;

public class ImportRepoCreateEffect : Effect<ImportRepoCreateAction>
{

    private readonly IRepo<Import> _importRepo;

    public ImportRepoCreateEffect(IRepo<Import> importRepo) => _importRepo = importRepo;

    public override async Task HandleAsync(ImportRepoCreateAction action, IDispatcher dispatcher) => 
        await _importRepo.Create(action.Imports);
}