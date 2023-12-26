namespace Presentation.Store.Import;

public record ImportCreateAction(IEnumerable<Domain.Import> Imports);