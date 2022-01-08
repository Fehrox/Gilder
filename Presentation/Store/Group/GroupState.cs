using Domain;

namespace Presentation.Store;

public record GroupState
{
    public GroupState(IEnumerable<Group> groups) => 
        Groups = groups.ToList();

    public IEnumerable<Group> Groups { get; set; }
}