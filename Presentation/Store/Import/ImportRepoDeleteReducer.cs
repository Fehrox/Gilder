using Fluxor;
using Presentation.Store.Imports;

namespace Presentation.Store.Import;

public class ImportRepoDeleteReducer : Reducer<ImportState, ImportDeleteAction>
{
    public override ImportState Reduce(ImportState state, ImportDeleteAction action)
    {
        var updatedImports = state.Imports.Except(action.Imports);
        return new ImportState(Imports: updatedImports.ToList());
    }
}