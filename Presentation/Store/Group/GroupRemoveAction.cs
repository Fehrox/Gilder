namespace Presentation.Store.Group;

public record GroupRemoveAction(IEnumerable<Domain.Group> GroupsToRemove);