using Application;
using Domain;
using Fluxor;

namespace Presentation.Store.Imports;

public class ImportRepoLoadEffect : Effect<ImportRepoLoadAction>
{
    private readonly IRepo<Import> _importRepo;

    public ImportRepoLoadEffect(IRepo<Import> importRepo) => 
        _importRepo = importRepo;

    public override async Task HandleAsync(ImportRepoLoadAction action, IDispatcher dispatcher)
    {
        var imports = await _importRepo.Read();
        dispatcher.Dispatch(new ImportCreateAction(imports));
    }
}