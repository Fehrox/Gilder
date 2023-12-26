namespace Presentation.Store.Import;

public record ImportRepoCreateAction(IEnumerable<Domain.Import> Imports);