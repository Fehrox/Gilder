using Fluxor;

namespace Presentation.Store.Group;

public record GroupCreateAction(IEnumerable<Domain.Group> Groups);

public class GroupCreateReducer : Reducer<GroupState, GroupCreateAction>
{
    public override GroupState Reduce(GroupState state, GroupCreateAction action) 
        => new(state.Groups.Concat(action.Groups));
}