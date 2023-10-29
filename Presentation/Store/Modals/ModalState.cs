namespace Portal.Store.Modals;

public record ModalState
{
    public Dictionary<Guid, KeyValuePair<Type, Dictionary<string, object>>> RenderingModals { get; set; } = new();
}