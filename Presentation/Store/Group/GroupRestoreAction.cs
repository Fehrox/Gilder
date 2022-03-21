namespace Presentation.Store.Group;

public record GroupRestoreAction(IEnumerable<Domain.Group> Groups);