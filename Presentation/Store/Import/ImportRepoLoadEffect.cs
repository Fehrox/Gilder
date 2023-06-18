using Application;
using Domain;
using Fluxor;

namespace Presentation.Store.Imports;

public class ImportRepoLoadEffect : Effect<ImportRepoLoadAction>
{
    private readonly IRepo<Domain.Import> _importRepo;

    public ImportRepoLoadEffect(IRepo<Domain.Import> importRepo) => 
        _importRepo = importRepo;

    public override async Task HandleAsync(ImportRepoLoadAction action, IDispatcher dispatcher)
    {
        var imports = await _importRepo.Read();
        dispatcher.Dispatch(new ImportCreateAction(imports));
    }
}