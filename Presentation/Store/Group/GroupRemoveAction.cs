using Domain;

namespace Presentation.Store;

public record GroupRemoveAction(IEnumerable<Group> GroupsToRemove);