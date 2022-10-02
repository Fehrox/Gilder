using Domain;

namespace Presentation.Store.Imports;

public record ImportRepoCreateAction(IEnumerable<Import> Imports);