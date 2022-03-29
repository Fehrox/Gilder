using Fluxor;

namespace Presentation.Store.Insights;

public class InsightsFeature : Feature<InsightsState>
{
    public override string GetName() => nameof(InsightsFeature);

    protected override InsightsState GetInitialState()
    {
        return new InsightsState()
        {
            
        };
    }
}