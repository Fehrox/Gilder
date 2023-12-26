using Application;
using Domain;
using Fluxor;

namespace Presentation.Store.Reviews;

public record ReviewDeleteAction(IEnumerable<Domain.Review> Reviews);

public class ReviewDeleteReducer : Reducer<ReviewState, ReviewDeleteAction>
{
    public override ReviewState Reduce(ReviewState state, ReviewDeleteAction action)
    {
        return state with {
            Reviews = state.Reviews.Where(r => action.Reviews.Any(ar => ar.Id != r.Id )) 
        };
    }
}

public class ReviewRepoDeleteEffect : Effect<ReviewDeleteAction>
{
    private readonly IRepo<Review> _reviewRepo;

    public ReviewRepoDeleteEffect(IRepo<Review> reviewRepo) => _reviewRepo = reviewRepo;

    public override async Task HandleAsync(ReviewDeleteAction action, IDispatcher dispatcher)
    {
        await _reviewRepo.Delete(action.Reviews);
    }
}