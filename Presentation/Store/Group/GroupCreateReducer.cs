using Fluxor;

namespace Presentation.Store.Group;

public class GroupCreateReducer : Reducer<GroupState, GroupCreateAction>
{
    public override GroupState Reduce(GroupState state, GroupCreateAction action) 
        => new(state.Groups.Concat(action.Groups));
}