using Fluxor;

namespace Presentation.Store.Imports;

public class ImportClearReducer : Reducer<ImportState, ImportClearAction>
{
    public override ImportState Reduce(ImportState state, ImportClearAction action) =>
            new(new List<Domain.Import>());
}