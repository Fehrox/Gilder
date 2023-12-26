using Fluxor;

namespace Presentation.Store.Reviews;

public class ReviewFeature : Feature<ReviewState>
{
    public override string GetName() => nameof(ReviewFeature);

    protected override ReviewState GetInitialState() => new();
}