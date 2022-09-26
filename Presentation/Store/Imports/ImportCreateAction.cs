using Domain;

namespace Presentation.Store.Imports;

public record ImportCreateAction(IEnumerable<Import> Imports);