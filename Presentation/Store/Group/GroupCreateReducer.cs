using Fluxor;

namespace Presentation.Store;

public class GroupCreateReducer : Reducer<GroupState, GroupCreateAction>
{
    public override GroupState Reduce(GroupState state, GroupCreateAction action) 
        => new(state.Groups.Concat(action.Groups));
}