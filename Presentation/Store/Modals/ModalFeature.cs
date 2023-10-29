using Fluxor;

namespace Portal.Store.Modals;

public class ModalFeature : Feature<ModalState>
{
    public override string GetName() => nameof(ModalFeature);

    protected override ModalState GetInitialState() => new();
}