using Domain;
using Fluxor;

namespace Presentation.Store.Imports;

public class ImportFeature : Feature<ImportState>
{
    public override string GetName() => nameof(ImportFeature);

    protected override ImportState GetInitialState() => new(new List<Import>());
}