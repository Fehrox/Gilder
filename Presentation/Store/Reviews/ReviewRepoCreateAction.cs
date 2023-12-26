namespace Presentation.Store.Reviews;

public record ReviewRepoCreateAction(IEnumerable<Domain.Review> Reviews);