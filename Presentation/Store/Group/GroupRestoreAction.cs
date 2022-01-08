using Domain;

namespace Presentation.Store;

public record GroupRestoreAction(IEnumerable<Group> Groups);