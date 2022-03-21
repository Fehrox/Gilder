using Fluxor;

namespace Presentation.Store.Group;

public class GroupFeature : Feature<GroupState>
{
    public override string GetName() => nameof(GroupFeature);

    protected override GroupState GetInitialState() => new(new List<Domain.Group>());
}