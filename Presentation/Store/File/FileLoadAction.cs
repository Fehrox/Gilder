using Microsoft.AspNetCore.Components.Forms;

namespace Presentation.Store.File;

public record FileLoadAction(IBrowserFile FileToLoad);