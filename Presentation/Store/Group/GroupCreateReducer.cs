using Fluxor;

namespace Presentation.Store.Group;

public class GroupCreateReducer : Reducer<GroupState, GroupRepoCreateAction>
{
    public override GroupState Reduce(GroupState state, GroupRepoCreateAction action) 
        => new(state.Groups.Concat(action.Groups));
}