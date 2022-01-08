using Domain;
using Fluxor;

namespace Presentation.Store;

public class GroupFeature : Feature<GroupState>
{
    public override string GetName() => nameof(GroupFeature);

    protected override GroupState GetInitialState() => new(new List<Group>());
}