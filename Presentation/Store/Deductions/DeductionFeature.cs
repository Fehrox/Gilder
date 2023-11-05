using Fluxor;

namespace Presentation.Store.Deductions;

public class DeductionFeature : Feature<DeductionState>
{
    public override string GetName() => nameof(DeductionFeature);

    protected override DeductionState GetInitialState() => new();
}