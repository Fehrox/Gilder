using Domain;

namespace Presentation.Store.Imports;

public record ImportState(IEnumerable<Import> Imports) { }