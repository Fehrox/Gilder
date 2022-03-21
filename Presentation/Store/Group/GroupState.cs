namespace Presentation.Store.Group;

public record GroupState
{
    public GroupState(IEnumerable<Domain.Group> groups) => 
        Groups = groups.ToList();

    public IEnumerable<Domain.Group> Groups { get; set; }
}