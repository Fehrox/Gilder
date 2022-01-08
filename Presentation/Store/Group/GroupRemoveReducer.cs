using Domain;
using Fluxor;

namespace Presentation.Store;

public class GroupRemoveReducer : Reducer<GroupState, GroupRemoveAction>
{
    public override GroupState Reduce(GroupState state, GroupRemoveAction action)
    {

        var groupsWithoutRemoved = state.Groups
            .Where(group => !action.GroupsToRemove.Contains(group))
            .ToList();

        return state with {
            Groups = groupsWithoutRemoved
        };
    }
}