using Domain;
using Fluxor;

namespace Presentation.Store.Reviews;

public record ReviewSetAction(IEnumerable<Review> Reviews);

public class ReviewSetReducer : Reducer<ReviewState, ReviewSetAction>
{
    public override ReviewState Reduce(ReviewState state, ReviewSetAction action) =>
        state with {
            Reviews = action.Reviews
        };
}   