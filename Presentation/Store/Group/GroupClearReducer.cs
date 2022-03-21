using Fluxor;

namespace Presentation.Store.Group; 

public class GroupClearReducer : Reducer<GroupState, GroupClearAction>
{
    public override GroupState Reduce(GroupState state, GroupClearAction action)
    {
        return new GroupState(new List<Domain.Group>());
    }
}