namespace Presentation.Store.Import;

public record ImportState(IEnumerable<Domain.Import> Imports) { }