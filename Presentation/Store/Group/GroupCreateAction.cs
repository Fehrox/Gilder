using Domain;

namespace Presentation.Store;

public record GroupCreateAction(IEnumerable<Group> Groups);