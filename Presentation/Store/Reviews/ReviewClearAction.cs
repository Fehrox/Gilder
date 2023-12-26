using Fluxor;

namespace Presentation.Store.Reviews;

public record ReviewClearAction();

public class ReviewClearReducer : Reducer<ReviewState, ReviewClearAction>
{
    public override ReviewState Reduce(ReviewState state, ReviewClearAction action) => new();
}