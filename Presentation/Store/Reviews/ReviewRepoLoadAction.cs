using Application;
using Domain;
using Fluxor;

namespace Presentation.Store.Reviews;

public record ReviewRepoLoadAction;

public class ReviewRepoLoadEffect : Effect<ReviewRepoLoadAction>
{
    private readonly IRepo<Review> _reviewRepo;

    public ReviewRepoLoadEffect(IRepo<Review> reviewRepo)
    {
        _reviewRepo = reviewRepo;
    }

    public override async Task HandleAsync(ReviewRepoLoadAction action, IDispatcher dispatcher)
    {
        var reviews = await _reviewRepo.Read();
        dispatcher.Dispatch(new ReviewSetAction(reviews));
    }
}