using Domain;

namespace Presentation.Store.Imports;

public record ImportState(IEnumerable<Domain.Import> Imports) { }