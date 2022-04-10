using Fluxor;

namespace Presentation.Store.Spending;

public class SpendingFeature : Feature<SpendingState>
{
    public override string GetName() => nameof(SpendingFeature);

    protected override SpendingState GetInitialState()
    {
        return new SpendingState()
        {
            
        };
    }
}