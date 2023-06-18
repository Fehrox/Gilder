using Application;
using Fluxor;

namespace Presentation.Store.Import;

public class ImportRepoDeleteEffect : Effect<ImportDeleteAction>
{
    private readonly IRepo<Domain.Import> _importRepo;

    public ImportRepoDeleteEffect(IRepo<Domain.Import> importRepo) => _importRepo = importRepo;

    public override async Task HandleAsync(ImportDeleteAction action, IDispatcher dispatcher) => 
        await _importRepo.Remove(action.Imports);
}