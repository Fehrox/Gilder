using Application;
using Domain;
using Fluxor;

namespace Presentation.Store.Reviews;

public record ReviewCreateAction(IEnumerable<Domain.Review> Reviews);

public class ReviewCreateReducer : Reducer<ReviewState, ReviewCreateAction>
{
    public override ReviewState Reduce(ReviewState state, ReviewCreateAction action) =>
        state with {
            Reviews = state.Reviews.Concat(action.Reviews)
        };
}

public class ReviewRepoCreateEffect : Effect<ReviewCreateAction>
{
    private readonly IRepo<Domain.Review> _reviewRepo;

    public ReviewRepoCreateEffect(IRepo<Review> reviewRepo) => _reviewRepo = reviewRepo;

    public override async Task HandleAsync(ReviewCreateAction action, IDispatcher dispatcher) => 
        await _reviewRepo.Create(action.Reviews);
}