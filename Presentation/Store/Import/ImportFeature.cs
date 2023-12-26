using Fluxor;

namespace Presentation.Store.Import;

public class ImportFeature : Feature<ImportState>
{
    public override string GetName() => nameof(ImportFeature);

    protected override ImportState GetInitialState() => new(new List<Domain.Import>());
}