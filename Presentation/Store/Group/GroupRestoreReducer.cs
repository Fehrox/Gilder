using Fluxor;

namespace Presentation.Store;

public class GroupRestoreReducer : Reducer<GroupState, GroupRestoreAction>
{
    public override GroupState Reduce(GroupState state, GroupRestoreAction action) => new(action.Groups);
}