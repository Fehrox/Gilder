using Fluxor;

namespace Presentation.Store.Imports;

public class ImportCreateReducer : Reducer<ImportState, ImportCreateAction>{
    public override ImportState Reduce(ImportState state, ImportCreateAction action) => 
        new(state.Imports.Concat(action.Imports));
}