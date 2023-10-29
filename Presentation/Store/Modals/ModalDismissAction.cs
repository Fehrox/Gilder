using Fluxor;

namespace Portal.Store.Modals;

public record ModalDismissAction(Guid ModalKey);

public class ModalDismissActionReducer : Reducer<ModalState, ModalDismissAction>
{
    public override ModalState Reduce(ModalState state, ModalDismissAction action)
    {
        state.RenderingModals.Remove(action.ModalKey);

        return state with {
            RenderingModals = new Dictionary<Guid, KeyValuePair<Type, Dictionary<string, object>>> (state.RenderingModals)
        };
    }
}