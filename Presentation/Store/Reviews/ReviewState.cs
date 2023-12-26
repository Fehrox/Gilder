using Domain;

namespace Presentation.Store.Reviews;

public record ReviewState
{
    public IEnumerable<Review> Reviews { get; set; } = new List<Review>();
}